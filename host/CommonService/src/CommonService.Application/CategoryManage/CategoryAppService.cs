using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonService.CategoryManage.Dtos;
using CommonService.Permissions;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;

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
    private readonly IDataFilter _dataFilter;

    public CategoryAppService(ICategoryRepository repository, ITagRepository tagRepository, IDataFilter dataFilter) : base(repository)
    {
        _repository = repository;
        _tagRepository = tagRepository;
        _dataFilter = dataFilter;
    }

    public override async Task<CategoryDto> CreateAsync(CategoryCreateUpdateDto input)
    {
        if (!await _repository.AnyAsync(x => x.Name.Equals(input.Name)))
        {
            return await base.CreateAsync(input);
        }
        throw new UserFriendlyException("已存在相同的类别名称", "500");
    }

    public async Task<List<CategoryTreeDto>> GetCategoryTrees(PageListAndSortedRequestDto input)
    {
        input.Sorting = !string.IsNullOrWhiteSpace(input.Sorting) ? input.Sorting : nameof(CategoryTreeDto.CreationTime);
        if (input.IsAll)
        {
            using (_dataFilter.Disable<ISoftDelete>())
            {
                var data = await _repository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, input.Sorting);
                return data.Select(item =>
                 {
                     var tags = _tagRepository.GetListAsync(x => x.ParentCategoryId.Equals(item.Id)).Result;
                     var tmp = ObjectMapper.Map<Category, CategoryTreeDto>(item);
                     tmp.Children = ObjectMapper.Map<List<Tag>, List<CategoryTreeDto>>(tags);
                     return tmp;
                 }).ToList();
            }
        }
        else
        {
            var data = await _repository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, input.Sorting);
            return data.Select(item =>
            {
                var tags = _tagRepository.GetListAsync(x => x.ParentCategoryId.Equals(item.Id)).Result;
                var tmp = ObjectMapper.Map<Category, CategoryTreeDto>(item);
                tmp.Children = ObjectMapper.Map<List<Tag>, List<CategoryTreeDto>>(tags);
                return tmp;
            }).ToList();
        }

    }
}
