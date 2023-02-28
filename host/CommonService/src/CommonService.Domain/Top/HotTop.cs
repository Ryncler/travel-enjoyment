using CommonService.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace CommonService.Top
{
    public class HotTop : FullAuditedEntity<Guid>
    {
        public Guid LinkId { get; private set; }

        public HotTopType TopType { get; private set; }

        public int Weight { get; private set; }

        protected HotTop()
        {
        }

        public HotTop(
            Guid id,
            Guid linkId,
            HotTopType topType,
            int weight
        ) : base(id)
        {
            LinkId = linkId;
            TopType = topType;
            Weight = weight;
        }
    }
}
