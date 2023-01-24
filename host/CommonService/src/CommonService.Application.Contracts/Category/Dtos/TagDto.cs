using System;
using Volo.Abp.Application.Dtos;

namespace CommonService.CategoryManage.Dtos;

[Serializable]
public class TagDto : FullAuditedEntityDto<Guid>
{
    public string Name { get; set; }

    public Guid ParentCategoryId { get; set; }
}