using System;
using BaseService.Permissions;
using BaseService.Entities.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using BaseService.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using BaseService.Enums;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Linq;
using Volo.Abp;
using Volo.Abp.Data;
using BaseService.User;

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

    public override async Task<EntryInfoDto> UpdateAsync(Guid id, EntryInfoCreateUpdateDto input)
    {
        var entryInfo = await Repository.GetAsync(id);
        if (entryInfo == null)
            throw new UserFriendlyException("Œ¥’“µΩ∏√…Í«Î", "500");

        entryInfo.UpdateStringInfo(input.CompanyName, input.UnifiedCreditCode);
        var result = await Repository.UpdateAsync(entryInfo);
        return MapToGetOutputDto(result);
    }

    public List<EnumInfoDto> GetApplyStatus()
    {
        var result = new List<EnumInfoDto>();
        foreach (Enum item in Enum.GetValues(typeof(ApplyStatus)))
        {
            result.Add(new EnumInfoDto
            {
                Id = Convert.ToInt32(item),
                Name = Enum.GetName(typeof(ApplyStatus), item),
                Description = GetEnumDesc(item)
            });
        }
        return result;
    }

    private string GetEnumDesc(Enum value)
    {
        var result = value.ToString();
        var fiald = value.GetType().GetField(result);
        var tmp = fiald.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
        if (tmp == null)
            return result;

        result = tmp.Description;
        return result;
    }
}
