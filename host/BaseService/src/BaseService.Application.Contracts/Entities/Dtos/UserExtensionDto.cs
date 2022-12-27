using System;
using Volo.Abp.Application.Dtos;

namespace BaseService.Entities.Dtos;

[Serializable]
public class UserExtensionDto : AuditedEntityDto
{
    public Guid UserId { get; set; }

    public string Avatar { get; set; }

    public string Profile { get; set; }

    public bool IsPushPrivateMessage { get; set; }

    public bool IsPushCommentMessage { get; set; }
}