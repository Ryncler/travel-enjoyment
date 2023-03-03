using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommonService.User.Dtos;
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
    Task<List<UserTravelDto>> GetListByUserId(string id);
}