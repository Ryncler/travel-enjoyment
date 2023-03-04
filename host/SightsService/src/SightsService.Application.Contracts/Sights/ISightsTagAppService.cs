using SightsService.SightsManage.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace SightsService.SightsManage;

public interface ISightsTagAppService :
    ICrudAppService<
                SightsTagDto,
        SightsTagKey,
        PagedAndSortedResultRequestDto,
        SightsTagCreateUpdateDto,
        SightsTagCreateUpdateDto>
{
    Task<List<SightsTagDto>> GetAllBySightsId(string id);

    Task<List<SightsTagDto>> CreateManyAsync(List<SightsTagCreateUpdateDto> input);

    Task DeleteManyAsync(List<SightsTagCreateUpdateDto> input);
}