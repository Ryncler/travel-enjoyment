using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace BaseService.User.Dtos
{
    public class UserDto : AuditedEntityDto<Guid>
    {
        public string UserName { get; set; }

        public bool Sex { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public List<string> Roles { get; set; }

        public bool Active { get; set; }

        public string Avatar { get; set; }

        public bool Delete { get; set; }
    }
}
