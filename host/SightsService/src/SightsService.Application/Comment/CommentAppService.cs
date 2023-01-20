using System;
using SightsService.Permissions;
using SightsService.CommentManage.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace SightsService.CommentManage;


public class CommentAppService : CrudAppService<Comment, CommentDto, Guid, PageListAndSortedRequestDto, CommentCreateUpdateDto, CommentCreateUpdateDto>,
    ICommentAppService
{
    //protected override string GetPolicyName { get; set; } = SightsServicePermissions.Comment.Default;
    //protected override string GetListPolicyName { get; set; } = SightsServicePermissions.Comment.Default;
    //protected override string CreatePolicyName { get; set; } = SightsServicePermissions.Comment.Create;
    //protected override string UpdatePolicyName { get; set; } = SightsServicePermissions.Comment.Update;
    //protected override string DeletePolicyName { get; set; } = SightsServicePermissions.Comment.Delete;

    private readonly ICommentRepository _repository;

    public CommentAppService(ICommentRepository repository) : base(repository)
    {
        _repository = repository;
    }

}
