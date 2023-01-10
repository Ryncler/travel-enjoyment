using BaseService.OpenIddicts;
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
    [Route("api/openiddict-manage")]
    public class OpenIddictManageController : BaseServiceController, IOpenIddictManageAppService
    {
        private readonly IOpenIddictManageAppService _openIddictManageAppService;

        public OpenIddictManageController(IOpenIddictManageAppService openIddictManageAppService)
        {
            _openIddictManageAppService = openIddictManageAppService;
        }

        [HttpPost]
        [Route("create-application")]
        public Task<OpenIddictApplicationDto> CreateApplicationAsync(OpenIddictApplicationCreateUpdateDto input)
        {
            return _openIddictManageAppService.CreateApplicationAsync(input);
        }

        [HttpPost]
        [Route("create-scope")]
        public Task<OpenIddictScopeDto> CreateScopeAsync(OpenIddictScopeCreateDto input)
        {
            return _openIddictManageAppService.CreateScopeAsync(input);
        }

        [HttpPost]
        [Route("delete-application")]
        public Task DeleteApplicationAsync(string id)
        {
            return _openIddictManageAppService.DeleteApplicationAsync(id);
        }

        [HttpPost]
        [Route("delete-scope")]
        public Task DeleteScopeAsync(string id)
        {
            return _openIddictManageAppService.DeleteScopeAsync(id);
        }

        [HttpGet]
        [Route("all-application")]
        public Task<PagedResultDto<OpenIddictApplicationDto>> GetAllApplicationAsync(PageListAndSortedRequestDto input)
        {
            return _openIddictManageAppService.GetAllApplicationAsync(input);
        }

        [HttpGet]
        [Route("all-scope")]
        public Task<PagedResultDto<OpenIddictScopeDto>> GetAllScopeAsync(PageListAndSortedRequestDto input)
        {
            return _openIddictManageAppService.GetAllScopeAsync(input);
        }

        [HttpGet]
        [Route("get-application")]
        public Task<OpenIddictApplicationDto> GetApplicationAsync(string id)
        {
            return _openIddictManageAppService.GetApplicationAsync(id);
        }

        [HttpGet]
        [Route("get-client-types")]
        public List<string> GetClientTypes()
        {
            return _openIddictManageAppService.GetClientTypes();
        }

        [HttpGet]
        [Route("get-consent-types")]
        public List<string> GetConsentTypes()
        {
            return _openIddictManageAppService.GetConsentTypes();
        }

        [HttpGet]
        [Route("get-grant-types")]
        public List<string> GetGrantTypes()
        {
            return _openIddictManageAppService.GetGrantTypes();
        }

        [HttpGet]
        [Route("get-scope")]
        public Task<OpenIddictScopeDto> GetScopeAsync(string id)
        {
            return _openIddictManageAppService.GetScopeAsync(id);
        }

        [HttpPost]
        [Route("update-application")]
        public Task<OpenIddictApplicationDto> UpdateApplicationAsync(string id, OpenIddictApplicationCreateUpdateDto input)
        {
            return _openIddictManageAppService.UpdateApplicationAsync(id, input);
        }

        [HttpPost]
        [Route("update-scope")]
        public Task<OpenIddictScopeDto> UpdateScopeAsync(string id, OpenIddictScopeUpdateDto input)
        {
            return _openIddictManageAppService.UpdateScopeAsync(id, input);
        }
    }
}
