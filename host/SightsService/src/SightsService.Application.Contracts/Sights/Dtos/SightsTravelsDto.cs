using System;
using Volo.Abp.Application.Dtos;

namespace SightsService.SightsManage.Dtos;

[Serializable]
public class SightsTravelsDto : EntityDto
{
    public Guid SightsId { get; set; }

    public Guid TravelsId { get; set; }
}