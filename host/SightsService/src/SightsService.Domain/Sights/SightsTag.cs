using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace SightsService.SightsManage
{
    public class SightsTag : Entity
    {
        public Guid SightsId { get; protected set; }

        public Guid TagId { get; protected set; }
        public override object[] GetKeys()
        {
            return new object[] { SightsId, TagId };
        }

        protected SightsTag()
        {
        }

        public SightsTag(
            Guid sightsId,
            Guid tagId
        )
        {
            SightsId = sightsId;
            TagId = tagId;
        }
    }
}
