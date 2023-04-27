using System;
using CommonService.Enum;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommonService.Top.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CommonService.Top;

public interface IHotTopAppService :
    ICrudAppService< 
                HotTopDto, 
        Guid, 
        PagedAndSortedResultRequestDto,
        HotTopCreateUpdateDto,
        HotTopCreateUpdateDto>
{
    Task<List<HotTopDto>> GetByHotTopTypeAsync(HotTopType topType);
}