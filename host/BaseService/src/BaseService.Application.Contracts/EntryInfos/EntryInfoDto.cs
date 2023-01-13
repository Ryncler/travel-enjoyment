using BaseService.Enums;
using System;
using Volo.Abp.Application.Dtos;

namespace BaseService.EntryInfos;

[Serializable]
public class EntryInfoDto : FullAuditedEntityDto<Guid>
{
    public Guid UserId { get; set; }

    public string CompanyName { get; set; }

    public string UnifiedCreditCode { get; set; }

    public DateTime? ApplyTiem { get; set; }

    public ApplyStatus Status { get; set; }

    public string FailedDescription { get; set; }
}