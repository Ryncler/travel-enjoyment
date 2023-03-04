using System;

namespace SightsService.SightsManage.Dtos;

[Serializable]
public class SightsTagCreateUpdateDto
{
    public Guid SightsId { get; set; }

    public string TagName { get; set; }
}