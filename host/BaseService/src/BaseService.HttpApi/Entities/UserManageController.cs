
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
    [Route("api/user-manage")]
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
        [Route("all")]
        public Task<PagedResultDto<UserDto>> GetAllAsync(PageListAndSortedRequestDto input)
        {
            return _userManageAppService.GetAllAsync(input);
        }

        [HttpGet]
        [Route("get-entrys")]
        public Task<PagedResultDto<UserEntryInfoDto>> GetAllEntryInfo(PageListAndSortedRequestDto input)
        {
            return _userManageAppService.GetAllEntryInfo(input);
        }

        [HttpGet]
        [Route("get")]
        public Task<UserDto> GetAsync(string id)
        {
            return _userManageAppService.GetAsync(id);
        }

        [HttpPost]
        [Route("register-entry")]
        public Task RegisterByEntry(RegisterUserByEntryDto input)
        {
            return _userManageAppService.RegisterByEntry(input);
        }

        [HttpPost]
        [Route("update")]
        public Task<UserDto> UpdateAsync(string id, UserCreateUpdateDto input)
        {
            return _userManageAppService.UpdateAsync(id, input);
        }

        [HttpPost]
        [Route("update-password")]
        public Task UpdateUserPassWord(UserPasswordDto input)
        {
            return _userManageAppService.UpdateUserPassWord(input);
        }
    }
}
