using System;
using Volo.Abp.Application.Dtos;

namespace SightsService.SightsManage.Dtos;

[Serializable]
public class SightsActivityDto : EntityDto
{
    public Guid SightsId { get; set; }

    public Guid ActivityId { get; set; }
}