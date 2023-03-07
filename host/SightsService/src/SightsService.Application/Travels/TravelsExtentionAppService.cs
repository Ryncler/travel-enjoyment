using System;
using SightsService.Permissions;
using SightsService.TravelsManage.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace SightsService.TravelsManage;


public class TravelsExtentionAppService : CrudAppService<TravelsExtention, TravelsExtentionDto, Guid, PageListAndSortedRequestDto, TravelsExtentionCreateUpdateDto, TravelsExtentionCreateUpdateDto>,
    ITravelsExtentionAppService
{
    //protected override string GetPolicyName { get; set; } = SightsServicePermissions.TravelsExtention.Default;
    //protected override string GetListPolicyName { get; set; } = SightsServicePermissions.TravelsExtention.Default;
    //protected override string CreatePolicyName { get; set; } = SightsServicePermissions.TravelsExtention.Create;
    //protected override string UpdatePolicyName { get; set; } = SightsServicePermissions.TravelsExtention.Update;
    //protected override string DeletePolicyName { get; set; } = SightsServicePermissions.TravelsExtention.Delete;

    private readonly ITravelsExtentionRepository _repository;

    public TravelsExtentionAppService(ITravelsExtentionRepository repository) : base(repository)
    {
        _repository = repository;
    }

}
