using SightsService.MinGans;
using System;
using System.ComponentModel;

namespace SightsService.TravelsManage.Dtos;

[Serializable]
public class TravelsCreateUpdateDto
{
    [DisplayName("TravelsReleaseId")]
    public Guid ReleaseId { get; set; }

    [DisplayName("TravelsReleaseDate")]
    public string ReleaseDate { get; set; } = DateTime.Now.ToString();

    [DisplayName("TravelsReleaseSite")]
    public string ReleaseSite { get; set; }

    [DisplayName("TravelsTravelsTitle")]
    public string TravelsTitle { get; set; }

    [DisplayName("TravelsDepartureTime")]
    public string DepartureTime { get; set; }

    [DisplayName("TravelsTravelDayNum")]
    public int TravelDayNum { get; set; }

    [DisplayName("TravelsTravelExpenses")]
    public decimal TravelExpenses { get; set; }

    [MinGanCheck]
    [DisplayName("TravelsTravelsContent")]
    public string Content { get; set; }
}