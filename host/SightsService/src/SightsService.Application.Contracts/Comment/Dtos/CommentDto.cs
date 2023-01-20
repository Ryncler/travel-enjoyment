using System;
using Volo.Abp.Application.Dtos;

namespace SightsService.CommentManage.Dtos;

[Serializable]
public class CommentDto : FullAuditedEntityDto<Guid>
{
    public Guid TravelsId { get; set; }

    public Guid UserId { get; set; }

    public DateTime ReleaseDate { get; set; }

    public Guid? ParentId { get; set; }

    public string Content { get; set; }
}