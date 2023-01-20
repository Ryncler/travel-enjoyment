using System;
using SightsService.Permissions;
using SightsService.ActivityManage.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace SightsService.ActivityManage;


public class ActivityAppService : CrudAppService<Activity, ActivityDto, Guid, PageListAndSortedRequestDto, ActivityCreateUpdateDto, ActivityCreateUpdateDto>,
    IActivityAppService
{
    //protected override string GetPolicyName { get; set; } = SightsServicePermissions.Activity.Default;
    //protected override string GetListPolicyName { get; set; } = SightsServicePermissions.Activity.Default;
    //protected override string CreatePolicyName { get; set; } = SightsServicePermissions.Activity.Create;
    //protected override string UpdatePolicyName { get; set; } = SightsServicePermissions.Activity.Update;
    //protected override string DeletePolicyName { get; set; } = SightsServicePermissions.Activity.Delete;

    private readonly IActivityRepository _repository;

    public ActivityAppService(IActivityRepository repository) : base(repository)
    {
        _repository = repository;
    }

}
