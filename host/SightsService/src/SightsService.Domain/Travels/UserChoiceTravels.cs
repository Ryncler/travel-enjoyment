using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace SightsService.TravelsManage
{
    public class UserChoiceTravels : FullAuditedEntity<Guid>
    {
        public Guid OneTravelsId { get; protected set; }

        public Guid TwoTravelsId { get; protected set; }

        protected UserChoiceTravels()
        {
        }

        public UserChoiceTravels(
            Guid id,
            Guid oneTravelsId,
            Guid twoTravelsId
        ) : base(id)
        {
            OneTravelsId = oneTravelsId;
            TwoTravelsId = twoTravelsId;
        }
    }
}
