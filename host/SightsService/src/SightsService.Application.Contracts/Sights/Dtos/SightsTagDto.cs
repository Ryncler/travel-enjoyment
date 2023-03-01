using System;
using Volo.Abp.Application.Dtos;

namespace SightsService.SightsManage.Dtos;

[Serializable]
public class SightsTagDto : EntityDto
{
    public Guid SightsId { get; set; }

    public Guid TagId { get; set; }
}