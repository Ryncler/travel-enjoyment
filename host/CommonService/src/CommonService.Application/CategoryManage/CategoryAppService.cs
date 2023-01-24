using System;
using CommonService.CategoryManage.Dtos;
using CommonService.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CommonService.CategoryManage;


public class CategoryAppService : CrudAppService<Category, CategoryDto, Guid, PagedAndSortedResultRequestDto, CategoryCreateUpdateDto, CategoryCreateUpdateDto>,
    ICategoryAppService
{
    //protected override string GetPolicyName { get; set; } = CommonServicePermissions.Category.Default;
    //protected override string GetListPolicyName { get; set; } = CommonServicePermissions.Category.Default;
    //protected override string CreatePolicyName { get; set; } = CommonServicePermissions.Category.Create;
    //protected override string UpdatePolicyName { get; set; } = CommonServicePermissions.Category.Update;
    //protected override string DeletePolicyName { get; set; } = CommonServicePermissions.Category.Delete;

    private readonly ICategoryRepository _repository;

    public CategoryAppService(ICategoryRepository repository) : base(repository)
    {
        _repository = repository;
    }

}
