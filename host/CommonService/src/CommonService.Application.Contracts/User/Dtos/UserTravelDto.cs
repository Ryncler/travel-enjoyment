using System;
using Volo.Abp.Application.Dtos;

namespace CommonService.User.Dtos;

[Serializable]
public class UserTravelDto : FullAuditedEntityDto<Guid>
{
    public Guid UserId { get; set; }

    public Guid TravelId { get; set; }
}