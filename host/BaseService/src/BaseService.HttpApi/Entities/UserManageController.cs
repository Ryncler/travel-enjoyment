
using BaseService.User;
using BaseService.User.Dtos;
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
    [Route("api/BaseService/usermanage")]
    public class UserManageController : BaseServiceController, IUserManageAppService
    {
        private readonly IUserManageAppService _userManageAppService;
        public UserManageController(IUserManageAppService userManageAppService)
        {
            _userManageAppService = userManageAppService;
        }

        [HttpPost]
        [Route("create")]
        public Task<UserDto> CreateAsync(UserCreateUpdateDto input)
        {
            return _userManageAppService.CreateAsync(input);
        }

        [HttpPost]
        [Route("delete")]
        public Task DeleteAsync(string id)
        {
            return _userManageAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("getall")]
        public Task<PagedResultDto<UserDto>> GetAllAsync(PageListAndSortedRequestDto input)
        {
            return _userManageAppService.GetAllAsync(input);
        }

        [HttpGet]
        [Route("get")]
        public Task<UserDto> GetAsync(string id)
        {
            return _userManageAppService.GetAsync(id);
        }

        [HttpPost]
        [Route("update")]
        public Task<UserDto> UpdateAsync(string id, UserCreateUpdateDto input)
        {
            return _userManageAppService.UpdateAsync(id, input);
        }
    }
}
