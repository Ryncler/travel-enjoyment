using System;
using System.ComponentModel;

namespace SightsService.TravelsManage.Dtos;

[Serializable]
public class TravelsExtentionCreateUpdateDto
{
    [DisplayName("TravelsExtentionTravelsId")]
    public Guid TravelsId { get; set; }

    [DisplayName("TravelsExtentionCalorificValue")]
    public int CalorificValue { get; set; }

    [DisplayName("TravelsExtentionStarValue")]
    public int StarValue { get; set; }
}