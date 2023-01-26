using System;
using System.Threading.Tasks;
using CommonService.CategoryManage.Dtos;
using CommonService.Permissions;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace CommonService.CategoryManage;


public class TagAppService : CrudAppService<Tag, TagDto, Guid, PagedAndSortedResultRequestDto, TagCreateUpdateDto, TagCreateUpdateDto>,
    ITagAppService
{
    //protected override string GetPolicyName { get; set; } = CommonServicePermissions.Tag.Default;
    //protected override string GetListPolicyName { get; set; } = CommonServicePermissions.Tag.Default;
    //protected override string CreatePolicyName { get; set; } = CommonServicePermissions.Tag.Create;
    //protected override string UpdatePolicyName { get; set; } = CommonServicePermissions.Tag.Update;
    //protected override string DeletePolicyName { get; set; } = CommonServicePermissions.Tag.Delete;

    private readonly ITagRepository _repository;

    public TagAppService(ITagRepository repository) : base(repository)
    {
        _repository = repository;
    }

    public override async Task<TagDto> CreateAsync(TagCreateUpdateDto input)
    {
        if (!await _repository.AnyAsync(x => x.Name.Equals(input.Name) && x.ParentCategoryId.Equals(input.ParentCategoryId)))
        {
            return await base.CreateAsync(input);
        }
        throw new UserFriendlyException("该类别下已存在相同的标签名称", "500");
    }
}
