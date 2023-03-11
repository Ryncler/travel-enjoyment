using SightsService.TravelsManage.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace SightsService.TravelsManage;

public interface IUserChoiceTravelsAppService :
    ICrudAppService<
                UserChoiceTravelsDto,
        Guid,
        PagedAndSortedResultRequestDto,
        UserChoiceTravelsCreateUpdateDto,
        UserChoiceTravelsCreateUpdateDto>
{
    Task<UserChoiceTravelListDto> GetChoiceTravel(string id);
}