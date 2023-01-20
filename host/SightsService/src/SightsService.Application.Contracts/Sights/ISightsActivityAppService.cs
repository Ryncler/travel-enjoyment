using System;
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

}