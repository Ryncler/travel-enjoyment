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
        public Task DeleteByTravelIdAndUserId(string userId, string travelId)
        {
            return _userTravelAppService.DeleteByTravelIdAndUserId(userId, travelId);
        }

        [HttpGet]
        [Route("get")]
        public Task<UserTravelDto> GetAsync(Guid id)
        {
            return _userTravelAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("get-count-by-travel")]
        public Task<int> GetCountByTravelId(string id)
        {
            return _userTravelAppService.GetCountByTravelId(id);
        }

        [HttpGet]
        [Route("get-count-by-user")]
        public Task<int> GetCountByUserId(string id)
        {
            return _userTravelAppService.GetCountByUserId(id);
        }

        [HttpGet]
        [Route("all")]
        public Task<PagedResultDto<UserTravelDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return _userTravelAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("all-by-travel")]
        public Task<List<UserTravelDto>> GetListByTravelId(string id)
        {
            return _userTravelAppService.GetListByTravelId(id);
        }

        [HttpGet]
        [Route("all-by-user")]
        public Task<List<UserTravelDto>> GetListByUserId(PagedUserTravelsByUserDto input)
        {
            return _userTravelAppService.GetListByUserId(input);
        }

        [HttpPost]
        [Route("update")]
        public Task<UserTravelDto> UpdateAsync(Guid id, UserTravelCreateUpdateDto input)
        {
            return _userTravelAppService.UpdateAsync(id, input);
        }
    }
}
