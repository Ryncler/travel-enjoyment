using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommonService.CategoryManage.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CommonService.CategoryManage;

public interface ITagAppService :
    ICrudAppService< 
                TagDto, 
        Guid, 
        PagedAndSortedResultRequestDto,
        TagCreateUpdateDto,
        TagCreateUpdateDto>
{
    Task<List<TagDto>> GetTagListByIdsAsync(List<string> ids);
}