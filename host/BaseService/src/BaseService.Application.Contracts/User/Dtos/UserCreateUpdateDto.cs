using System;
using System.Collections.Generic;
using System.Text;

namespace BaseService.User.Dtos
{
    public class UserCreateUpdateDto
    {
        public string UserName { get; set; }

        public bool Sex { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public List<string> Roles { get; set; }

        public bool Active { get; set; }

        public string Profile { get; set; }

        public string Password { get; set; }

        public string Avatar { get; set; }
    }
}
