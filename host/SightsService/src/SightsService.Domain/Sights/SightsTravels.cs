using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace SightsService.SightsManage
{
    public class SightsTravels : Entity
    {
        public Guid SightsId { get; protected set; }

        public Guid TravelsId { get; protected set; }

        public override object[] GetKeys()
        {
            return new object[] { SightsId, TravelsId };
        }

        protected SightsTravels()
        {
        }

        public SightsTravels(
            Guid sightsId,
            Guid travelsId
        )
        {
            SightsId = sightsId;
            TravelsId = travelsId;
        }
    }
}
