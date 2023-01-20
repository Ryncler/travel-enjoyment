using System;
using System.ComponentModel;

namespace SightsService.TravelsManage.Dtos;

[Serializable]
public class TravelsCreateUpdateDto
{
    [DisplayName("TravelsReleaseId")]
    public Guid ReleaseId { get; set; }

    [DisplayName("TravelsReleaseDate")]
    public DateTime ReleaseDate { get; set; }

    [DisplayName("TravelsReleaseSite")]
    public string ReleaseSite { get; set; }

    [DisplayName("TravelsTravelsTitle")]
    public string TravelsTitle { get; set; }

    [DisplayName("TravelsDepartureTime")]
    public DateTime DepartureTime { get; set; }

    [DisplayName("TravelsTravelDayNum")]
    public int TravelDayNum { get; set; }

    [DisplayName("TravelsTravelExpenses")]
    public decimal TravelExpenses { get; set; }

    [DisplayName("TravelsTravelsContent")]
    public string TravelsContent { get; set; }
}