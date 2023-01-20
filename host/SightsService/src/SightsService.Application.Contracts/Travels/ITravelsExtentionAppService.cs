using System;
using SightsService.TravelsManage.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace SightsService.TravelsManage;

public interface ITravelsExtentionAppService :
    ICrudAppService< 
                TravelsExtentionDto, 
        Guid,
        PageListAndSortedRequestDto,
        TravelsExtentionCreateUpdateDto,
        TravelsExtentionCreateUpdateDto>
{

}