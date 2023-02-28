using CommonService.Enum;
using System;
using Volo.Abp.Application.Dtos;

namespace CommonService.Top.Dtos;

[Serializable]
public class HotTopDto : FullAuditedEntityDto<Guid>
{
    public Guid LinkId { get; set; }

    public HotTopType TopType { get; set; }

    public int Weight { get; set; }
}