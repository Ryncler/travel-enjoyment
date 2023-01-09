﻿using BaseService.OpenIddicts;
using BaseService.OpenIddicts.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace BaseService.Entities
{
    [Area(BaseServiceRemoteServiceConsts.ModuleName)]
    [RemoteService(Name = BaseServiceRemoteServiceConsts.RemoteServiceName)]
    [Route("api/BaseService/openiddictmanage")]
    public class OpenIddictManageController : BaseServiceController, IOpenIddictManageAppService
    {
        private readonly IOpenIddictManageAppService _openIddictManageAppService;

        public OpenIddictManageController(IOpenIddictManageAppService openIddictManageAppService)
        {
            _openIddictManageAppService = openIddictManageAppService;
        }

        [HttpPost]
        [Route("createapplication")]
        public Task<OpenIddictApplicationDto> CreateApplicationAsync(OpenIddictApplicationCreateUpdateDto input)
        {
            return _openIddictManageAppService.CreateApplicationAsync(input);
        }

        [HttpPost]
        [Route("createscope")]
        public Task<OpenIddictScopeDto> CreateScopeAsync(OpenIddictScopeCreateDto input)
        {
            return _openIddictManageAppService.CreateScopeAsync(input);
        }

        [HttpPost]
        [Route("deleteapplication")]
        public Task DeleteApplicationAsync(string id)
        {
            return _openIddictManageAppService.DeleteApplicationAsync(id);
        }

        [HttpPost]
        [Route("deletescope")]
        public Task DeleteScopeAsync(string id)
        {
            return _openIddictManageAppService.DeleteScopeAsync(id);
        }

        [HttpGet]
        [Route("getallapplication")]
        public Task<PagedResultDto<OpenIddictApplicationDto>> GetAllApplicationAsync(PageListAndSortedRequestDto input)
        {
            return _openIddictManageAppService.GetAllApplicationAsync(input);
        }

        [HttpGet]
        [Route("getallscope")]
        public Task<PagedResultDto<OpenIddictScopeDto>> GetAllScopeAsync(PageListAndSortedRequestDto input)
        {
            return _openIddictManageAppService.GetAllScopeAsync(input);
        }

        [HttpGet]
        [Route("getapplication")]
        public Task<OpenIddictApplicationDto> GetApplicationAsync(string id)
        {
            return _openIddictManageAppService.GetApplicationAsync(id);
        }

        [HttpGet]
        [Route("getclienttypes")]
        public List<string> GetClientTypes()
        {
            return _openIddictManageAppService.GetClientTypes();
        }

        [HttpGet]
        [Route("getconsenttypes")]
        public List<string> GetConsentTypes()
        {
            return _openIddictManageAppService.GetConsentTypes();
        }

        [HttpGet]
        [Route("getgranttypes")]
        public List<string> GetGrantTypes()
        {
            return _openIddictManageAppService.GetGrantTypes();
        }

        [HttpGet]
        [Route("getscope")]
        public Task<OpenIddictScopeDto> GetScopeAsync(string id)
        {
            return _openIddictManageAppService.GetScopeAsync(id);
        }

        [HttpPost]
        [Route("updateapplication")]
        public Task<OpenIddictApplicationDto> UpdateApplicationAsync(string id, OpenIddictApplicationCreateUpdateDto input)
        {
            return _openIddictManageAppService.UpdateApplicationAsync(id, input);
        }

        [HttpPost]
        [Route("updatescope")]
        public Task<OpenIddictScopeDto> UpdateScopeAsync(string id, OpenIddictScopeUpdateDto input)
        {
            return _openIddictManageAppService.UpdateScopeAsync(id, input);
        }
    }
}
