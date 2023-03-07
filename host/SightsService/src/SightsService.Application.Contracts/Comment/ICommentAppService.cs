using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SightsService.CommentManage.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace SightsService.CommentManage;

public interface ICommentAppService :
    ICrudAppService<
                CommentDto,
        Guid,
        PageListAndSortedRequestDto,
        CommentCreateUpdateDto,
        CommentCreateUpdateDto>
{
    Task<List<CommentDto>> GetAllByTravelId(string id);

    Task<int> GetTravelCommentCount(string id);

    Task<PagedResultDto<CommentTreeDto>> GetPagedCommentTreeByTravelIdAsync(PagedCommentByTravelDto input);
}