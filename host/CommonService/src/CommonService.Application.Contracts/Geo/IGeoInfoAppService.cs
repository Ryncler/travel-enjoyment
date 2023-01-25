using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommonService.Geo.Dtos;
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
    Task<bool> CreateMany(List<GeoInfoCreateUpdateDto> inputs);

    Task<List<GeoTreeDto>> GetGeoTreeAsync();
}