using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace SightsService.SightsManage
{
    public class SightsTag : Entity
    {
        public Guid SightsId { get; protected set; }

        public string TagName { get; protected set; }

        public override object[] GetKeys()
        {
            return new object[] { SightsId };
        }

        protected SightsTag()
        {
        }

        public SightsTag(
            Guid sightsId,
            string tagName
        )
        {
            SightsId = sightsId;
            TagName = tagName;
        }
    }
}
