using System;
using Volo.Abp.Application.Dtos;

namespace CommonService.GeoManage.Dtos;

[Serializable]
public class GeoInfoDto : FullAuditedEntityDto<Guid>
{
    public int Code { get; set; }

    public string Name { get; set; }

    public string Province { get; set; }

    public string City { get; set; }

    public string Aera { get; set; }

    public string Town { get; set; }
}