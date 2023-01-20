using System;
using Volo.Abp.Application.Dtos;

namespace SightsService.TravelsManage.Dtos;

[Serializable]
public class TravelsExtentionDto : FullAuditedEntityDto<Guid>
{
    public Guid TravelsId { get; set; }

    public int CalorificValue { get; set; }

    public int StarValue { get; set; }
}