using SightsService.User.Dtos;
using SightsService.UserManage.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace SightsService.UserManage
{
    public interface IUserTrendsAppService : IApplicationService
    {
        Task<PagedResultDto<UserTrendsDto>> GetPagedTrendsByUserAsync(PagedUserTrendsByUserDto input);
    }
}
