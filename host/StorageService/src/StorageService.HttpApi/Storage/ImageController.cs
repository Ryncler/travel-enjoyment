﻿using Microsoft.AspNetCore.Mvc;
using StorageService.Minio;
using StorageService.Storage.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace StorageService.Storage
{
    [Area(StorageServiceRemoteServiceConsts.ModuleName)]
    [RemoteService(Name = StorageServiceRemoteServiceConsts.RemoteServiceName)]
    [Route("api/image-manage")]
    public class ImageController : StorageServiceController, IImageAppService
    {
        private readonly IImageAppService _imageAppService;
        public ImageController(IImageAppService imageAppService)
        {
            _imageAppService = imageAppService;
        }

        [HttpPost]
        [Route("create")]
        public Task<ImageDto> CreateAsync(ImageCreateUpdateDto input)
        {
            return _imageAppService.CreateAsync(input);
        }

        [HttpPost]
        [Route("delete")]
        public Task DeleteAsync(Guid id)
        {
            return _imageAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("get")]
        public Task<ImageDto> GetAsync(Guid id)
        {
            return _imageAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("all")]
        public Task<PagedResultDto<ImageDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return _imageAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("all-by-linkid")]
        public Task<List<ImageDto>> GetListByLinkId(string linkId)
        {
            return _imageAppService.GetListByLinkId(linkId);
        }

        [HttpPost]
        [Route("update")]
        public Task<ImageDto> UpdateAsync(Guid id, ImageCreateUpdateDto input)
        {
            return _imageAppService.UpdateAsync(id, input);
        }
    }
}