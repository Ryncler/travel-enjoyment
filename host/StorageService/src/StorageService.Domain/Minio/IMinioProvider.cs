using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace StorageService.Minio
{
    public interface IMinioProvider
    {
        Task<MinioProviderResult> UploadAsync(MinioProviderArgs args);

        Task<List<MinioProviderResult>> UploadManyAsync(IEnumerable<MinioProviderArgs> args);

        Task<MinioProviderResult> DeleteAsync(MinioProviderArgs args);

        Task<List<MinioProviderResult>> DeleteManyAsync(IEnumerable<MinioProviderArgs> args);

        Task<bool> ExistsAsync(MinioProviderArgs args);

        Task<Stream> GetOrNullAsync(MinioProviderArgs args);

        List<string> GetAllByPrefix(string prefix, string bucketName, bool recursive = true);

        Task CreateBucketIfNotExists(string containerName);
    }
}
