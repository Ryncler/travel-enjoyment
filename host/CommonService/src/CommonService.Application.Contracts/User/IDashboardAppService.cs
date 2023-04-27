using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommonService.User.Dtos;
using CommonService.UserManage.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace CommonService.User;

public interface IDashboardAppService : ITransientDependency
{
    Task<List<RecentHotDto>> GetRecentHotSightsAsync();

    Task<List<RecentHotDto>> GetRecentHotTagsAsync();

    Task<List<object>> GetNewlyInfoAsync();

    Task<List<TravelsDto>> GetRecentHotTravelAsync();

    Task<List<RecentHotUserDto>> GetRecentHotUserAsync();
}