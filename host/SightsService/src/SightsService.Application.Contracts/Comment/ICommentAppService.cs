using System;
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

}