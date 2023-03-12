using Microsoft.AspNetCore.Mvc;
using SightsService.User.Dtos;
using SightsService.UserManage.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace SightsService.UserManage
{
    [Area(SightsServiceRemoteServiceConsts.ModuleName)]
    [RemoteService(Name = SightsServiceRemoteServiceConsts.RemoteServiceName)]
    [Route("api/user-trends-manage")]
    public class UserTrendsController : SightsServiceController, IUserTrendsAppService
    {
        private readonly IUserTrendsAppService _userTrendsAppService;
        public UserTrendsController(IUserTrendsAppService userTrendsAppService)
        {
            _userTrendsAppService = userTrendsAppService;
        }

        [HttpGet]
        [Route("all-by-user")]
        public Task<PagedResultDto<UserTrendsDto>> GetPagedTrendsByUserAsync(PagedUserTrendsByUserDto input)
        {
            return _userTrendsAppService.GetPagedTrendsByUserAsync(input);
        }
    }
}
