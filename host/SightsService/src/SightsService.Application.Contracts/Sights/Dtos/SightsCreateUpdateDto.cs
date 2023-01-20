using System;
using System.ComponentModel;

namespace SightsService.SightsManage.Dtos;

[Serializable]
public class SightsCreateUpdateDto
{
    [DisplayName("SightsName")]
    public string Name { get; set; }

    [DisplayName("SightsChangeTime")]
    public DateTime ChangeTime { get; set; }

    [DisplayName("SightsMapId")]
    public Guid MapId { get; set; }

    [DisplayName("SightsDescription")]
    public string Description { get; set; }

    [DisplayName("SightsOpenTime")]
    public DateTime OpenTime { get; set; }

    [DisplayName("SightsAddress")]
    public string Address { get; set; }

    [DisplayName("SightsTicket")]
    public string Ticket { get; set; }

    [DisplayName("SightsTrafficGuide")]
    public string TrafficGuide { get; set; }

    [DisplayName("SightsSelfDrivingGuide")]
    public string SelfDrivingGuide { get; set; }
}