using System;
using SightsService.Permissions;
using SightsService.ActivityManage.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using System.Threading.Tasks;
using Volo.Abp.Data;
using SightsService.SightsManage.Dtos;
using System.Collections.Generic;
using Volo.Abp;
using Microsoft.AspNetCore.Authorization;

namespace SightsService.ActivityManage;


public class ActivityAppService : CrudAppService<Activity, ActivityDto, Guid, PageListAndSortedRequestDto, ActivityCreateUpdateDto, ActivityCreateUpdateDto>,
    IActivityAppService
{
    protected override string GetPolicyName { get; set; } = SightsServicePermissions.Activity.Default;
    protected override string GetListPolicyName { get; set; } = SightsServicePermissions.Activity.Default;
    protected override string CreatePolicyName { get; set; } = SightsServicePermissions.Activity.Create;
    protected override string UpdatePolicyName { get; set; } = SightsServicePermissions.Activity.Update;
    protected override string DeletePolicyName { get; set; } = SightsServicePermissions.Activity.Delete;

    private readonly IActivityRepository _repository;
    private readonly IDataFilter _dataFilter;

    public ActivityAppService(IActivityRepository repository, IDataFilter dataFilter) : base(repository)
    {
        _repository = repository;
        _dataFilter = dataFilter;
    }

    [Authorize(SightsServicePermissions.Activity.Default)]
    public async Task<PagedResultDto<ActivityDto>> GetListByCreateIdAsync(Guid createId, PageListAndSortedRequestDto input)
    {
        if (createId.Equals(Guid.Empty))
            return new PagedResultDto<ActivityDto>
            {
                Items = null,
                TotalCount = 0
            };

        input.Sorting = !string.IsNullOrWhiteSpace(input.Sorting) ? input.Sorting : nameof(ActivityDto.CreationTime);
        if (input.IsAll)
        {
            using (_dataFilter.Disable<ISoftDelete>())
            {
                var data = await _repository.GetActivityByCreateIdAsync(createId, input.SkipCount, input.MaxResultCount, input.Sorting);
                return new PagedResultDto<ActivityDto>
                {
                    Items = ObjectMapper.Map<List<Activity>, List<ActivityDto>>(data),
                    TotalCount = await _repository.GetCountByCreateIdAsync(createId)
                };
            }
        }
        else
        {
            var data = await _repository.GetActivityByCreateIdAsync(createId, input.SkipCount, input.MaxResultCount, input.Sorting);
            return new PagedResultDto<ActivityDto>
            {
                Items = ObjectMapper.Map<List<Activity>, List<ActivityDto>>(data),
                TotalCount = await _repository.GetCountByCreateIdAsync(createId)
            };
        }
    }
    public async override Task<PagedResultDto<ActivityDto>> GetListAsync(PageListAndSortedRequestDto input)
    {
        input.Sorting = !string.IsNullOrWhiteSpace(input.Sorting) ? input.Sorting : nameof(SightsDto.CreationTime);
        if (input.IsAll)
        {
            using (_dataFilter.Disable<ISoftDelete>())
            {
                var data = await _repository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, input.Sorting);
                return new PagedResultDto<ActivityDto>
                {
                    Items = ObjectMapper.Map<List<Activity>, List<ActivityDto>>(data),
                    TotalCount = await _repository.GetCountAsync()
                };
            }
        }
        else
        {
            var data = await _repository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, input.Sorting);
            return new PagedResultDto<ActivityDto>
            {
                Items = ObjectMapper.Map<List<Activity>, List<ActivityDto>>(data),
                TotalCount = await _repository.GetCountAsync()
            };
        }
    }
}
