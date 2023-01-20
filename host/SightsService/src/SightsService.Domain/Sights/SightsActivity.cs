using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace SightsService.SightsManage
{
    public class SightsActivity : Entity
    {
        public Guid SightsId { get; protected set; }

        public Guid ActivityId { get; protected set; }

        public override object[] GetKeys()
        {
            return new object[] { SightsId, ActivityId };
        }

        protected SightsActivity()
        {
        }

        public SightsActivity(
            Guid sightsId,
            Guid activityId
        )
        {
            SightsId = sightsId;
            ActivityId = activityId;
        }
    }
}
