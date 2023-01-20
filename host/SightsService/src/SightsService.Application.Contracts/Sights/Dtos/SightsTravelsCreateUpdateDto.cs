using System;
using System.ComponentModel;

namespace SightsService.SightsManage.Dtos;

[Serializable]
public class SightsTravelsCreateUpdateDto
{
    [DisplayName("SightsTravelsSightsId")]
    public Guid SightsId { get; set; }

    [DisplayName("SightsTravelsTravelsId")]
    public Guid TravelsId { get; set; }
}