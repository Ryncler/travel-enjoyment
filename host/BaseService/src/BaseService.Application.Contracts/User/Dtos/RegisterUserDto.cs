using System;
using System.Collections.Generic;
using System.Text;

namespace BaseService.User.Dtos
{
    public class RegisterUserDto
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Password { get; set; }

        public string CompanyName { get; set; }

        public string UnifiedCreditCode { get; set; }
    }
}
