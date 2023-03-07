using System;
using System.Collections.Generic;
using System.Text;

namespace SightsService.CommentManage.Dtos
{
    public class PagedCommentByTravelDto : PageListAndSortedRequestDto
    {
        public string Id { get; set; }
    }
}
