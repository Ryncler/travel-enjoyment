using StorageService.Minio.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.ObjectMapping;

namespace StorageService.Minio
{
    public class MinioManageAppService : StorageServiceAppService, IMinioManageAppService
    {
        private readonly IMinioProvider _minioProvider;

        public MinioManageAppService(IMinioProvider minioProvider)
        {
            _minioProvider = minioProvider;
        }

        public async Task<bool> DeleteAsync(MinioDto input)
        {
            var result = await _minioProvider.DeleteAsync(ObjectMapper.Map<MinioDto, MinioProviderArgs>(input));
            return result.Result;
        }

        public async Task<bool> DeleteManyAsync(List<MinioDto> input)
        {
            var result = await _minioProvider.DeleteManyAsync(ObjectMapper.Map<List<MinioDto>, List<MinioProviderArgs>>(input));
            if (result.Any(x => x.Result == false))
                throw new UserFriendlyException("以下文件删除失败：" + result.Select(x => x.ErrorObjecName), "500");
            else
                return true;
        }

        public async Task<List<string>> GetAllByPrefix(MinioListDto input)
        {
            return await Task.Run(() =>
             {
                 return _minioProvider.GetAllByPrefix(input.Prefix, input.BucketName, input.IsRecursive);
             });
        }

        public async Task<Stream> GetAsync(MinioDto input)
        {
            return await _minioProvider.GetOrNullAsync(ObjectMapper.Map<MinioDto, MinioProviderArgs>(input));
        }

        public async Task<string> UploadAsync(MinioDto input)
        {
            var result = await _minioProvider.UploadAsync(ObjectMapper.Map<MinioDto, MinioProviderArgs>(input));
            if (!result.Result)
                throw new UserFriendlyException("上传失败,请重试", "500");
            return result.ObjectName;
        }

        public async Task<List<string>> UploadManyAsync(List<MinioDto> input)
        {
            var result = await _minioProvider.UploadManyAsync(ObjectMapper.Map<List<MinioDto>, List<MinioProviderArgs>>(input));
            if (result.Any(x => x.Result == false))
                throw new UserFriendlyException("以下文件上传失败：" + result.Select(x => x.ErrorObjecName), "500");
            else
                return result.Select(x => x.ObjectName).ToList();
        }
    }
}
