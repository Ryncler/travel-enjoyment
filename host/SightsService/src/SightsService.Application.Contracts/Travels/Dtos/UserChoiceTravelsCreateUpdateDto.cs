using System;

namespace SightsService.TravelsManage.Dtos;

[Serializable]
public class UserChoiceTravelsCreateUpdateDto
{
    public Guid OneTravelsId { get; set; }

    public Guid TwoTravelsId { get; set; }
}