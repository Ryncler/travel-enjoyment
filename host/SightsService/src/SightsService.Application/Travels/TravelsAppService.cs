using System;
using SightsService.Permissions;
using SightsService.TravelsManage.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace SightsService.TravelsManage;


public class TravelsAppService : CrudAppService<Travels, TravelsDto, Guid, PageListAndSortedRequestDto, TravelsCreateUpdateDto, TravelsCreateUpdateDto>,
    ITravelsAppService
{
    //protected override string GetPolicyName { get; set; } = SightsServicePermissions.Travels.Default;
    //protected override string GetListPolicyName { get; set; } = SightsServicePermissions.Travels.Default;
    //protected override string CreatePolicyName { get; set; } = SightsServicePermissions.Travels.Create;
    //protected override string UpdatePolicyName { get; set; } = SightsServicePermissions.Travels.Update;
    //protected override string DeletePolicyName { get; set; } = SightsServicePermissions.Travels.Delete;

    private readonly ITravelsRepository _repository;

    public TravelsAppService(ITravelsRepository repository) : base(repository)
    {
        _repository = repository;
    }

}
