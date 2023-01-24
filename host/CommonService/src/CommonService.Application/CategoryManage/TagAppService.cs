using System;
using CommonService.CategoryManage.Dtos;
using CommonService.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

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

}
