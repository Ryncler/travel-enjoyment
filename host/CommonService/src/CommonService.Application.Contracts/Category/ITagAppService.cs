using System;
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

}