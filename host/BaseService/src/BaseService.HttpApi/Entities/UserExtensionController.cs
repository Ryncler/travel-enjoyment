
using BaseService.Entities.Dtos;
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
    [Route("api/BaseService/userextension")]
    public class UserExtensionController : BaseServiceController, IUserExtensionAppService
    {
        private readonly IUserExtensionAppService _userExtensionAppService;

        public UserExtensionController(IUserExtensionAppService userExtensionAppService)
        {
            _userExtensionAppService = userExtensionAppService;
        }

        [HttpPost]
        [Route("create")]
        public Task<UserExtensionDto> CreateAsync(UserExtensionCreateDto input)
        {
            return _userExtensionAppService.CreateAsync(input);
        }

        [HttpPost]
        [Route("delete")]
        public Task DeleteAsync(UserExtensionKey id)
        {
            return _userExtensionAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("get")]
        public Task<UserExtensionDto> GetAsync(UserExtensionKey id)
        {
            return _userExtensionAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("getall")]
        public Task<PagedResultDto<UserExtensionDto>> GetListAsync(PageListAndSortedRequestDto input)
        {
            return _userExtensionAppService.GetListAsync(input);
        }

        [HttpPost]
        [Route("update")]
        public Task<UserExtensionDto> UpdateAsync(UserExtensionKey id, UserExtensionUpdateDto input)
        {
            return _userExtensionAppService.UpdateAsync(id, input);
        }
    }
}
