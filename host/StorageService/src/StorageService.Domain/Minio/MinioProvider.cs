using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel;
using Minio.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace StorageService.Minio
{
    public class MinioProvider : IMinioProvider, ITransientDependency
    {
        private readonly MinioClient _client;

        protected MinioNamingNormalizer _normalizer { get; }
        protected IOptions<AbpMinioOptions> _options { get; }

        public MinioProvider(MinioNamingNormalizer normalizer, IOptions<AbpMinioOptions> options)
        {
            _normalizer = normalizer;
            _options = options;
            _client = GetMinioClient();
        }

        protected virtual MinioClient GetMinioClient()
        {
            var configuration = _options.Value.Minio;

            var client = new MinioClient()
                .WithEndpoint(configuration.EndPoint)
                .WithCredentials(configuration.AccessKey, configuration.SecretKey)
                .WithRegion(configuration.Region);

            if (configuration.WithSSL)
            {
                client.WithSSL();
            }

            return client.Build();
        }

        public async Task<MinioProviderResult> DeleteAsync(MinioProviderArgs args)
        {
            try
            {
                await _client.RemoveObjectAsync(new RemoveObjectArgs().WithBucket(args.BucketName).WithObject(args.ObjectName));
                return new MinioProviderResult
                {
                    Result = true,
                    ObjectName = args.ObjectName
                };
            }
            catch
            {
                return new MinioProviderResult
                {
                    Result = false,
                    ErrorObjecName = args.ObjectName
                };
            }
        }

        public async Task<List<MinioProviderResult>> DeleteManyAsync(IEnumerable<MinioProviderArgs> args)
        {
            var result = new List<MinioProviderResult>();
            foreach (var item in args)
            {
                var deleteResult = await DeleteAsync(item);
                result.Add(deleteResult);
            }
            return result;
        }

        public async Task<bool> ExistsAsync(MinioProviderArgs args)
        {
            if (await _client.BucketExistsAsync(new BucketExistsArgs().WithBucket(args.BucketName)))
            {
                try
                {
                    await _client.StatObjectAsync(new StatObjectArgs().WithBucket(args.BucketName).WithObject(args.ObjectName));
                }
                catch (Exception e)
                {
                    if (e is ObjectNotFoundException)
                    {
                        return false;
                    }
                    throw;
                }
                return true;
            }
            return false;
        }

        public async Task<Stream> GetOrNullAsync(MinioProviderArgs args)
        {
            if (!await ExistsAsync(args))
            {
                return null;
            }
            var memoryStream = new MemoryStream();
            await _client.GetObjectAsync(new GetObjectArgs().WithBucket(args.BucketName).WithObject(args.ObjectName).WithCallbackStream(stream =>
            {
                if (stream != null)
                {
                    stream.CopyTo(memoryStream);
                    memoryStream.Seek(0, SeekOrigin.Begin);
                }
                else
                    memoryStream = null;
            }));
            return memoryStream;
        }

        public List<string> GetAllByPrefix(string prefix, string bucketName, bool recursive = true)
        {
            var result = new List<string>();
            var items = _client.ListObjectsAsync(new ListObjectsArgs().WithBucket(bucketName).WithPrefix(prefix).WithRecursive(recursive));
            IDisposable subscription = items.Subscribe
            (
               item => result.Add(item.Key),
               ex => throw ex
            );
            items.Wait();
            return result;
        }

        public async Task<MinioProviderResult> UploadAsync(MinioProviderArgs args)
        {
            args.BucketName = _normalizer.NormalizeBucketName(args.BucketName);
            args.ObjectName = _normalizer.NormalizeObjectName(args.ObjectName);
            if (!args.OverrideExisting && await ExistsAsync(args))
            {
                throw new UserFriendlyException($"文件名： '{args.ObjectName}' 已存在于 '{args.BucketName}'! 如果要覆盖内容，请设置 {nameof(args.OverrideExisting)} 为 true");
            }
            await CreateBucketIfNotExists(args.BucketName);
            try
            {
                await _client.PutObjectAsync(new PutObjectArgs()
                .WithBucket(args.BucketName)
                .WithObject(args.ObjectName)
                .WithStreamData(args.FileStream)
                .WithObjectSize(args.FileStream.Length));
                return new MinioProviderResult
                {
                    Result = true,
                    ObjectName = args.ObjectName,
                };
            }
            catch
            {
                return new MinioProviderResult
                {
                    Result = false,
                    ErrorObjecName = args.ObjectName
                };
            }
        }

        public async Task<List<MinioProviderResult>> UploadManyAsync(IEnumerable<MinioProviderArgs> args)
        {
            var result = new List<MinioProviderResult>();
            foreach (var item in args)
            {
                var uploadResult = await UploadAsync(item);
                result.Add(uploadResult);
            }
            return result;
        }

        public async Task CreateBucketIfNotExists(string bucketName)
        {
            if (!await _client.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucketName)))
            {
                await _client.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucketName));
            }
        }

    }
}
