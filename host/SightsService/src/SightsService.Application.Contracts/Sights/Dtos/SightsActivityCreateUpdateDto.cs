using System;
using System.ComponentModel;

namespace SightsService.SightsManage.Dtos;

[Serializable]
public class SightsActivityCreateUpdateDto
{
    [DisplayName("SightsActivitySightsId")]
    public Guid SightsId { get; set; }

    [DisplayName("SightsActivityActivityId")]
    public Guid ActivityId { get; set; }
}