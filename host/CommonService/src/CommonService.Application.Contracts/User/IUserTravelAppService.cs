using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommonService.User.Dtos;
using CommonService.UserManage.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CommonService.User;

public interface IUserTravelAppService :
    ICrudAppService<
                UserTravelDto,
        Guid,
        PagedAndSortedResultRequestDto,
        UserTravelCreateUpdateDto,
        UserTravelCreateUpdateDto>
{
    Task<List<UserTravelDto>> GetListByUserIdAsync(PagedUserTravelsByUserDto input);

    Task<List<UserTravelDto>> GetListByTravelIdAsync(string id);

    Task<int> GetCountByUserIdAsync(string id);

    Task<int> GetCountByTravelIdAsync(string id);

    Task DeleteByTravelIdAndUserIdAsync(string userId, string travelId);

    Task<bool> ExitsUserTravelAsync(string userId, string travelId);
}