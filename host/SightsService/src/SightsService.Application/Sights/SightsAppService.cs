using System;
using SightsService.Permissions;
using SightsService.SightsManage.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace SightsService.SightsManage;


public class SightsAppService : CrudAppService<Sights, SightsDto, Guid, PageListAndSortedRequestDto, SightsCreateUpdateDto, SightsCreateUpdateDto>,
    ISightsAppService
{
    //protected override string GetPolicyName { get; set; } = SightsServicePermissions.Sights.Default;
    //protected override string GetListPolicyName { get; set; } = SightsServicePermissions.Sights.Default;
    //protected override string CreatePolicyName { get; set; } = SightsServicePermissions.Sights.Create;
    //protected override string UpdatePolicyName { get; set; } = SightsServicePermissions.Sights.Update;
    //protected override string DeletePolicyName { get; set; } = SightsServicePermissions.Sights.Delete;

    private readonly ISightsRepository _repository;

    public SightsAppService(ISightsRepository repository) : base(repository)
    {
        _repository = repository;
    }

}
