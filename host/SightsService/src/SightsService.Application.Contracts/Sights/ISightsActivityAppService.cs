using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SightsService.SightsManage.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace SightsService.SightsManage;

public interface ISightsActivityAppService :
    ICrudAppService< 
                SightsActivityDto, 
        SightsActivityKey,
        PageListAndSortedRequestDto,
        SightsActivityCreateUpdateDto,
        SightsActivityCreateUpdateDto>
{
    Task<List<SightsActivityDto>> GetAllBySightsIdAsync(string id);
}