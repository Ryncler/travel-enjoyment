using System;
using CommonService.GeoManage.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CommonService.GeoManage;

public interface IGeoInfoAppService :
    ICrudAppService< 
                GeoInfoDto, 
        Guid, 
        PagedAndSortedResultRequestDto,
        GeoInfoCreateUpdateDto,
        GeoInfoCreateUpdateDto>
{

}