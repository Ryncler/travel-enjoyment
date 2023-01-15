using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace BaseService.EntryInfos;

public interface IEntryInfoManageAppService :
    ICrudAppService<
                EntryInfoDto,
        Guid,
        PageListAndSortedRequestDto,
        EntryInfoCreateUpdateDto,
        EntryInfoCreateUpdateDto>
{
    List<EnumInfoDto> GetApplyStatus();

    Task<EntryInfoDto> UpdateNameAndCodeAsync(UpdateNameAndCodeDto input);
}