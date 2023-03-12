using System;
using System.Collections.Generic;
using System.Text;

namespace SightsService.User.Dtos
{
    public class PagedUserTrendsByUserDto : PageListAndSortedRequestDto
    {
        public Guid UserId { get; set; }
    }
}
