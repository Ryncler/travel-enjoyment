using System;
using System.Collections.Generic;
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

    public override async Task<TagDto> UpdateAsync(Guid id, TagCreateUpdateDto input)
    {
        var tag = await _repository.FindAsync(id);
        if (tag != null)
        {
            if (!await _repository.AnyAsync(x => x.Name.Equals(input.Name) && x.ParentCategoryId.Equals(tag.ParentCategoryId)))
            {
                return await base.UpdateAsync(id, new TagCreateUpdateDto
                {
                    Name = input.Name,
                    ParentCategoryId = tag.ParentCategoryId
                });
            }
            throw new UserFriendlyException("该类别下已存在相同的标签名称", "500");
        }
        throw new UserFriendlyException("该类别下不存在该标签", "500");
    }

    public async Task<List<TagDto>> GetTagListByIdsAsync(List<string> ids)
    {
        var tags = await _repository.GetListByIds(ids);
        return ObjectMapper.Map<List<Tag>, List<TagDto>>(tags);
    }
}
