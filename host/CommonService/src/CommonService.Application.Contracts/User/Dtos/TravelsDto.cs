using System;
using Volo.Abp.Application.Dtos;

namespace CommonService.User.Dtos;

[Serializable]
public class TravelsDto : FullAuditedEntityDto<Guid>
{
    public Guid ReleaseId { get; set; }

    public DateTime ReleaseDate { get; set; }

    public string ReleaseSite { get; set; }

    public string TravelsTitle { get; set; }

    public DateTime DepartureTime { get; set; }

    public int TravelDayNum { get; set; }

    public decimal TravelExpenses { get; set; }

    public string Content { get; set; }
}