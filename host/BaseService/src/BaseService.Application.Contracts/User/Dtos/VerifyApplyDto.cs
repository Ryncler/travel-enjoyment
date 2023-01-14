using System;
using System.Collections.Generic;
using System.Text;

namespace BaseService.User.Dtos
{
    public class VerifyApplyDto
    {
        public string Id { get; set; }

        public bool IsSuccess { get; set; }

        public string Description { get; set; }
    }
}
