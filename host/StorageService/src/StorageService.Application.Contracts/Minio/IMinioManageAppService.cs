using StorageService.Minio.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace StorageService.Minio
{
    public interface IMinioManageAppService : IApplicationService
    {
        Task<string> UploadAsync(MinioDto input);

        Task<bool> DeleteAsync(MinioDto input);

        Task<bool> DeleteManyAsync(List<MinioDto> input);

        Task<Stream> GetAsync(MinioDto input);

        Task<List<string>> GetAllByPrefix(MinioListDto input);
    }
}
