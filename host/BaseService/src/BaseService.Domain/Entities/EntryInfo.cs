using BaseService.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace BaseService.Entities
{
    public class EntryInfo : FullAuditedAggregateRoot<Guid>
    {
        public Guid UserId { get; protected set; }

        public string CompanyName { get; protected set; }

        public string UnifiedCreditCode { get; protected set; }

        public DateTime? ApplyTiem { get; protected set; }

        public ApplyStatus Status { get; protected set; }

        public string FailedDescription { get; protected set; }

        protected EntryInfo()
        {
        }

        public EntryInfo(
            Guid id,
            Guid userId,
            string companyName,
            string unifiedCreditCode,
            DateTime? applyTiem,
            ApplyStatus status,
            string failedDescription
        ) : base(id)
        {
            UserId = userId;
            CompanyName = companyName;
            UnifiedCreditCode = unifiedCreditCode;
            ApplyTiem = applyTiem;
            Status = status;
            FailedDescription = failedDescription;
        }

        public void UpdateStringInfo(string companyName, string unifiedCreditCode)
        {
            CompanyName = companyName;
            UnifiedCreditCode = unifiedCreditCode;
            FailedDescription = "";
        }
    }
}
