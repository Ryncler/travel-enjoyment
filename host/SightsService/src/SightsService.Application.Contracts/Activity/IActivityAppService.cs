using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SightsService.ActivityManage.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace SightsService.ActivityManage;

public interface IActivityAppService :
    ICrudAppService<
                ActivityDto,
        Guid,
        PageListAndSortedRequestDto,
        ActivityCreateUpdateDto,
        ActivityCreateUpdateDto>
{
    Task<PagedResultDto<ActivityDto>> GetListByCreateIdAsync(Guid createId, PageListAndSortedRequestDto input);

    Task<List<ActivityDto>> GetActivityListByIdsAsync(List<string> ids);
}