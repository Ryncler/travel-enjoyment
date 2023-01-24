using System;
using CommonService.CategoryManage.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CommonService.CategoryManage;

public interface ICategoryAppService :
    ICrudAppService<
                CategoryDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CategoryCreateUpdateDto,
        CategoryCreateUpdateDto>
{

}