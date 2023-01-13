using System;
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

}