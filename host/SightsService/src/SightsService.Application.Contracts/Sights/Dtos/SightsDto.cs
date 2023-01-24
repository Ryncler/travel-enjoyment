using System;
using Volo.Abp.Application.Dtos;

namespace SightsService.SightsManage.Dtos;

[Serializable]
public class SightsDto : FullAuditedEntityDto<Guid>
{
    public string Name { get; set; }

    public Guid MapId { get; set; }

    public string Description { get; set; }

    public string OpenTime { get; set; }

    public string Address { get; set; }

    public string Ticket { get; set; }

    public string TrafficGuide { get; set; }

    public string SelfDrivingGuide { get; set; }
}