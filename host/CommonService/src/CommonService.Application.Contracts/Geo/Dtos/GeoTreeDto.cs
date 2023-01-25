using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace CommonService.Geo.Dtos
{
    public class GeoTreeDto : FullAuditedEntityDto<Guid>
    {
        public int Code { get; set; }

        public string Name { get; set; }

        public string Province { get; set; }

        public string City { get; set; }

        public string Area { get; set; }

        public List<GeoTreeDto> Children { get; set; }
    }
}
