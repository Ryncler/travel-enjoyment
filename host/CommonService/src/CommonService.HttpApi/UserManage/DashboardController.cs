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
    [Route("api/dashboard-manage")]
    public class DashboardController : CommonServiceController, IDashboardAppService
    {
        private readonly IDashboardAppService _dashboardAppService;

        public DashboardController(IDashboardAppService dashboardAppService)
        {
            _dashboardAppService = dashboardAppService;
        }

        [HttpGet]
        [Route("get-newly-info")]
        public Task<List<object>> GetNewlyInfo()
        {
            return _dashboardAppService.GetNewlyInfo();
        }

        [HttpGet]
        [Route("get-recent-hot-sights")]
        public Task<List<RecentHotDto>> GetRecentHotSights()
        {
            return _dashboardAppService.GetRecentHotSights();
        }

        [HttpGet]
        [Route("get-recent-hot-tags")]
        public Task<List<RecentHotDto>> GetRecentHotTags()
        {
            return _dashboardAppService.GetRecentHotTags();
        }

        [HttpGet]
        [Route("get-recent-hot-travel")]
        public Task<List<TravelsDto>> GetRecentHotTravel()
        {
            return _dashboardAppService.GetRecentHotTravel();
        }

        [HttpGet]
        [Route("get-recent-hot-user")]
        public Task<List<RecentHotUserDto>> GetRecentHotUser()
        {
            return _dashboardAppService.GetRecentHotUser();
        }
    }
}
