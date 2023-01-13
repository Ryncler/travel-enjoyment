using System;
using BaseService.Permissions;
using BaseService.Entities.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using BaseService.Entities;

namespace BaseService.EntryInfos;


public class EntryInfoManageAppService : CrudAppService<EntryInfo, EntryInfoDto, Guid, PageListAndSortedRequestDto, EntryInfoCreateUpdateDto, EntryInfoCreateUpdateDto>,
    IEntryInfoManageAppService
{
    //protected override string GetPolicyName { get; set; } = BaseServicePermissions.EntryInfo.Default;
    //protected override string GetListPolicyName { get; set; } = BaseServicePermissions.EntryInfo.Default;
    //protected override string CreatePolicyName { get; set; } = BaseServicePermissions.EntryInfo.Create;
    //protected override string UpdatePolicyName { get; set; } = BaseServicePermissions.EntryInfo.Update;
    //protected override string DeletePolicyName { get; set; } = BaseServicePermissions.EntryInfo.Delete;

    public EntryInfoManageAppService(IRepository<EntryInfo, Guid> repository) : base(repository)
    {
    }

}
