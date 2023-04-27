using CommonService.Top;
using CommonService.User;
using CommonService.User.Dtos;
using CommonService.UserManage.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace CommonService.UserManage
{
    [Area(CommonServiceRemoteServiceConsts.ModuleName)]
    [RemoteService(Name = CommonServiceRemoteServiceConsts.RemoteServiceName)]
    [Route("api/user-manage")]
    public class UserTravelController : CommonServiceController, IUserTravelAppService
    {
        private readonly IUserTravelAppService _userTravelAppService;

        public UserTravelController(IUserTravelAppService userSightsAppService)
        {
            _userTravelAppService = userSightsAppService;
        }

        [HttpPost]
        [Route("create")]
        public Task<UserTravelDto> CreateAsync(UserTravelCreateUpdateDto input)
        {
            return _userTravelAppService.CreateAsync(input);
        }

        [HttpPost]
        [Route("delete")]
        public Task DeleteAsync(Guid id)
        {
            return _userTravelAppService.DeleteAsync(id);
        }

        [HttpPost]
        [Route("delete-by-id")]
        public Task DeleteByTravelIdAndUserIdAsync(string userId, string travelId)
        {
            return _userTravelAppService.DeleteByTravelIdAndUserIdAsync(userId, travelId);
        }

        [HttpPost]
        [Route("exits-by-id")]
        public Task<bool> ExitsUserTravelAsync(string userId, string travelId)
        {
            return _userTravelAppService.ExitsUserTravelAsync(userId, travelId);
        }

        [HttpGet]
        [Route("get")]
        public Task<UserTravelDto> GetAsync(Guid id)
        {
            return _userTravelAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("get-count-by-travel")]
        public Task<int> GetCountByTravelIdAsync(string id)
        {
            return _userTravelAppService.GetCountByTravelIdAsync(id);
        }

        [HttpGet]
        [Route("get-count-by-user")]
        public Task<int> GetCountByUserIdAsync(string id)
        {
            return _userTravelAppService.GetCountByUserIdAsync(id);
        }

        [HttpGet]
        [Route("all")]
        public Task<PagedResultDto<UserTravelDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return _userTravelAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("all-by-travel")]
        public Task<List<UserTravelDto>> GetListByTravelIdAsync(string id)
        {
            return _userTravelAppService.GetListByTravelIdAsync(id);
        }

        [HttpGet]
        [Route("all-by-user")]
        public Task<List<UserTravelDto>> GetListByUserIdAsync(PagedUserTravelsByUserDto input)
        {
            return _userTravelAppService.GetListByUserIdAsync(input);
        }

        [HttpPost]
        [Route("update")]
        public Task<UserTravelDto> UpdateAsync(Guid id, UserTravelCreateUpdateDto input)
        {
            return _userTravelAppService.UpdateAsync(id, input);
        }
    }
}
