using System;
using System.Collections.Generic;
using System.Text;

namespace CommonService.UserManage.Dtos
{
    public class PagedUserTravelsByUserDto : PageListAndSortedRequestDto
    {
        public Guid UserId { get; set; }
    }
}
