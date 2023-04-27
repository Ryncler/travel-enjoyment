using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SightsService.SightsManage.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace SightsService.SightsManage;

public interface ISightsTravelsAppService :
    ICrudAppService< 
                SightsTravelsDto, 
        SightsTravelsKey,
        PageListAndSortedRequestDto,
        SightsTravelsCreateUpdateDto,
        SightsTravelsCreateUpdateDto>
{
    Task<List<SightsTravelsDto>> GetAllBySightsIdAsync(string id);
}