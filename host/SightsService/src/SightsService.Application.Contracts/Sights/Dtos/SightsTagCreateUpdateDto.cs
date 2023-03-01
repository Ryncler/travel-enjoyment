using System;

namespace SightsService.SightsManage.Dtos;

[Serializable]
public class SightsTagCreateUpdateDto
{
    public Guid SightsId { get; set; }

    public Guid TagId { get; set; }
}