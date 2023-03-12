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
    Task<List<UserTravelDto>> GetListByUserId(PagedUserTravelsByUserDto input);

    Task<List<UserTravelDto>> GetListByTravelId(string id);

    Task<int> GetCountByUserId(string id);

    Task<int> GetCountByTravelId(string id);

    Task DeleteByTravelIdAndUserId(string userId, string travelId);
}