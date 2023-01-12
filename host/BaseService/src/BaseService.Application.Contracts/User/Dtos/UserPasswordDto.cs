using System;
using System.Collections.Generic;
using System.Text;

namespace BaseService.User.Dtos
{
    public class UserPasswordDto
    {
        public string UserId { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }
    }
}
