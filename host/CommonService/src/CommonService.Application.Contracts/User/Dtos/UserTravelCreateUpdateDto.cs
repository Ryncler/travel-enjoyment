using System;

namespace CommonService.User.Dtos;

[Serializable]
public class UserTravelCreateUpdateDto
{
    public Guid UserId { get; set; }

    public Guid TravelId { get; set; }
}