using System;
using Volo.Abp.Application.Dtos;

namespace SightsService.TravelsManage.Dtos;

[Serializable]
public class UserChoiceTravelsDto : FullAuditedEntityDto<Guid>
{
    public Guid OneTravelsId { get; set; }

    public Guid TwoTravelsId { get; set; }
}