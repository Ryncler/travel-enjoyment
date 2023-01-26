using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    private readonly ITagRepository _tagRepository;

    public CategoryAppService(ICategoryRepository repository, ITagRepository tagRepository) : base(repository)
    {
        _repository = repository;
        _tagRepository = tagRepository;
    }

    public async Task<List<CategoryTreeDto>> GetCategoryTrees()
    {
        var data = await _repository.GetListAsync();
        return data.Select(item =>
         {
             var tags = _tagRepository.GetListAsync(x => x.ParentCategoryId.Equals(item.Id)).Result;
             var tmp = ObjectMapper.Map<Category, CategoryTreeDto>(item);
             tmp.Children = ObjectMapper.Map<List<Tag>, List<CategoryTreeDto>>(tags);
             return tmp;
         }).ToList();
    }
}
