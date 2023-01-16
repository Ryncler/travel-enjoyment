using Microsoft.AspNetCore.Mvc;
using StorageService.Minio.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp;

namespace StorageService.Minio
{
    [Area(StorageServiceRemoteServiceConsts.ModuleName)]
    [RemoteService(Name = StorageServiceRemoteServiceConsts.RemoteServiceName)]
    [Route("api/minio-manage")]
    public class MinioManageController : StorageServiceController, IMinioManageAppService
    {
        private readonly IMinioManageAppService _minioManageAppService;

        public MinioManageController(IMinioManageAppService minioManageAppService)
        {
            _minioManageAppService = minioManageAppService;
        }

        [HttpPost]
        [Route("delete")]
        public Task<bool> DeleteAsync(MinioDto input)
        {
            return _minioManageAppService.DeleteAsync(input);
        }

        [HttpPost]
        [Route("delete-many")]
        public Task<bool> DeleteManyAsync(List<MinioDto> input)
        {
            return _minioManageAppService.DeleteManyAsync(input);
        }

        [HttpGet]
        [Route("all")]
        public Task<List<string>> GetAllByPrefix(MinioListDto input)
        {
            return _minioManageAppService.GetAllByPrefix(input);
        }

        [HttpGet]
        [Route("get")]
        public Task<Stream> GetAsync(MinioDto input)
        {
            return _minioManageAppService.GetAsync(input);
        }

        [HttpPost]
        [Route("upload")]
        public Task<string> UploadAsync(MinioDto input)
        {
            return _minioManageAppService.UploadAsync(input);
        }

        [HttpPost]
        [Route("upload-many")]
        public Task<List<string>> UploadManyAsync(List<MinioDto> input)
        {
            return _minioManageAppService.UploadManyAsync(input);
        }
    }
}
