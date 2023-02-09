using BaseService.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseService.Email.Dtos
{
    public class SendDto
    {
        public string Email { get; set; }

        public string Subject { get; set; }

        public string Data { get; set; }
    }
}
