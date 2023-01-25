using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace CommonService.GeoManage
{
    public class GeoInfo : FullAuditedEntity<Guid>
    {
        public int Code { get; protected set; }

        public string Name { get; protected set; }

        public string Province { get; protected set; }

        public string City { get; protected set; }

        public string Area { get; protected set; }

        protected GeoInfo()
        {
        }

        public GeoInfo(
            Guid id,
            int code,
            string name,
            string province,
            string city,
            string area
        ) : base(id)
        {
            Code = code;
            Name = name;
            Province = province;
            City = city;
            Area = area;
        }
    }
}
