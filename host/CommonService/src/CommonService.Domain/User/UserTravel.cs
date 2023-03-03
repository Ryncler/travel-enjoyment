using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace CommonService.User
{
    public class UserTravel : FullAuditedEntity<Guid>
    {
        public Guid UserId { get; protected set; }

        public Guid TravelId { get; protected set; }

        protected UserTravel()
        {
        }

        public UserTravel(
            Guid id,
            Guid userId,
            Guid travelId
        ) : base(id)
        {
            UserId = userId;
            TravelId = travelId;
        }
    }
}
