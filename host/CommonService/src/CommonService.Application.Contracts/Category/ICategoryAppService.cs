using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    Task<PagedResultDto<CategoryTreeDto>> GetCategoryTreesAsync(PageListAndSortedRequestDto input);
}