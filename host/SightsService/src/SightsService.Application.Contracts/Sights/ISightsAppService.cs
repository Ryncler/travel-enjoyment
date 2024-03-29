using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SightsService.SightsManage.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace SightsService.SightsManage;

public interface ISightsAppService :
    ICrudAppService<
                SightsDto,
        Guid,
        PageListAndSortedRequestDto,
        SightsCreateUpdateDto,
        SightsCreateUpdateDto>
{
    Task<SightsDto> GetSightsByActivityIdAsync(string id);

    Task<List<SightsDto>> GetSightsBySearchAsync(SightsSerachDto input);

    Task<PagedResultDto<SightsDto>> GetListByCreateIdAsync(Guid createId, PageListAndSortedRequestDto input);

    Task<List<SightsDto>> GetSightsListByIdsAsync(List<string> ids);

    Task<List<SightsDto>> GetSightsByGeoAsync(string id);
}