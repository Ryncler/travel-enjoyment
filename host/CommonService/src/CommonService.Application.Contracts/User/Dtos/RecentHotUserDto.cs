using System;
using System.Collections.Generic;
using System.Text;

namespace CommonService.User.Dtos
{
    public class RecentHotUserDto
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public string Avatar { get; set; }

        public int TravelNum { get; set; }

        public int CommentNum { get; set; }

        public int StarNum { get; set; }

        public string LastTime { get; set; }
    }
}
