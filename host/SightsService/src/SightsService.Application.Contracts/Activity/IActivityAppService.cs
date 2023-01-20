using System;
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

}