using System;
using SightsService.Permissions;
using SightsService.CommentManage.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

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

    public async Task<List<CommentDto>> GetAllByTravelId(string id)
    {
        var result = await _repository.GetListAsync(x => x.TravelsId.Equals(Guid.Parse(id)));
        return ObjectMapper.Map<List<Comment>, List<CommentDto>>(result);
    }

    public async Task<int> GetTravelCommentCount(string id)
    {
        var result = await GetAllByTravelId(id);
        return result.Count;
    }

    public async Task<PagedResultDto<CommentTreeDto>> GetPagedCommentTreeByTravelIdAsync(PagedCommentByTravelDto input)
    {
        var query = await _repository.GetQueryableAsync();
        var total = (await _repository.GetListAsync(x => x.TravelsId.Equals(Guid.Parse(input.Id)))).Count;
        var pageQuery = query.Where(x => x.TravelsId.Equals(Guid.Parse(input.Id)) && x.ParentId.Equals(Guid.Empty))
                         .OrderByDescending(x => x.CreationTime)
                         .PageBy(input.SkipCount, input.MaxResultCount)
                         .AsEnumerable()
                         .Select(item =>
                         {
                             return ObjectMapper.Map<Comment, CommentTreeDto>(item);
                         })
                         .ToList();

        foreach (var item in pageQuery)
        {
            await TreeData(item, input.ChildrenCount);
        }
        return new PagedResultDto<CommentTreeDto>
        {
            Items = pageQuery,
            TotalCount = total
        };
    }

    public async Task<List<CommentDto>> GetCommentByParent(string id)
    {
        var result = new List<CommentDto>();
        var parent = await _repository.FindAsync(x => x.Id.Equals(Guid.Parse(id)));
        if (parent != null)
        {
            result.Add(MapToGetOutputDto(parent));
            await ListDto(result, parent);
        }

        return result;
    }

    protected async Task ListDto(List<CommentDto> result, Comment parent)
    {
        var comment = await _repository.FindAsync(x => x.ParentId.Equals(parent.Id));
        if (comment != null)
        {
            result.Add(MapToGetOutputDto(comment));
            await ListDto(result, comment);
        }
    }

    protected async Task TreeData(CommentTreeDto parent, int childrenCount)
    {
        var subItems = await _repository.GetListAsync(x => x.ParentId.Equals(parent.Id));
        if (subItems.Count > 0)
        {
            if (childrenCount != 0)
                subItems = subItems.Take(childrenCount).ToList();

            var data = ObjectMapper.Map<List<Comment>, List<CommentTreeDto>>(subItems);
            parent.Children = data;
            foreach (var item in data)
            {
                await TreeData(item, childrenCount);
            }
        }
    }
}
