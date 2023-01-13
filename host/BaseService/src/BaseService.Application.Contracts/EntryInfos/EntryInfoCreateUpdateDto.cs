using BaseService.Enums;
using System;
using System.ComponentModel;

namespace BaseService.EntryInfos;

[Serializable]
public class EntryInfoCreateUpdateDto
{
    [DisplayName("EntryInfoUserId")]
    public Guid UserId { get; set; }

    [DisplayName("EntryInfoCompanyName")]
    public string CompanyName { get; set; }

    [DisplayName("EntryInfoUnifiedCreditCode")]
    public string UnifiedCreditCode { get; set; }

    [DisplayName("EntryInfoApplyTiem")]
    public DateTime? ApplyTiem { get; set; }

    [DisplayName("EntryInfoStatus")]
    public ApplyStatus Status { get; set; }

    [DisplayName("EntryInfoFailedDescription")]
    public string FailedDescription { get; set; }
}