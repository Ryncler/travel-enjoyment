using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace SightsService.TravelsManage
{
    public class TravelsExtention : FullAuditedEntity<Guid>
    {
        public Guid TravelsId { get; protected set; }

        public int CalorificValue { get; protected set; }

        public int StarValue { get; protected set; }

        protected TravelsExtention()
        {
        }

        public TravelsExtention(
            Guid id,
            Guid travelsId,
            int calorificValue,
            int starValue
        ) : base(id)
        {
            TravelsId = travelsId;
            CalorificValue = calorificValue;
            StarValue = starValue;
        }
    }
}
