using System;
using Volo.Abp.Application.Dtos;

namespace SightsService.ActivityManage.Dtos;

[Serializable]
public class ActivityDto : FullAuditedEntityDto<Guid>
{
    public string Name { get; set; }

    public DateTime ChangeTime { get; set; }

    public string Content { get; set; }
}