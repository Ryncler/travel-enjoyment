using BaseService.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace BaseService.User.Dtos
{
    public class UserEntryInfoDto : FullAuditedEntityDto<Guid>
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public bool Active { get; set; }

        public string CompanyName { get; set; }

        public string UnifiedCreditCode { get; set; }

        public DateTime? ApplyTiem { get; set; }

        public ApplyStatus Status { get; set; }

        public string FailedDescription { get; set; }
    }
}
