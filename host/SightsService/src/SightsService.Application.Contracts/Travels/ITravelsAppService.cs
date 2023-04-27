using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SightsService.TravelsManage.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace SightsService.TravelsManage;

public interface ITravelsAppService :
    ICrudAppService< 
                TravelsDto, 
        Guid,
        PageListAndSortedRequestDto,
        TravelsCreateUpdateDto,
        TravelsCreateUpdateDto>
{
    Task<List<TravelsDto>> GetActivityListByIdsAsync(List<string> ids);

    Task<int> GetCountByUserIdAsync(string id);

    Task<PagedResultDto<TravelsDto>> GetPagedByCreateIdAsync(Guid createId, PageListAndSortedRequestDto input);
}