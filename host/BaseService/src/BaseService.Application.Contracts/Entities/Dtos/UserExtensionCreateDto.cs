using System;

namespace BaseService.Entities.Dtos;

[Serializable]
public class UserExtensionCreateDto
{
    public Guid UserId { get; set; }

    public string Avatar { get; set; }

    public string Profile { get; set; }

    public bool IsPushPrivateMessage { get; set; }

    public bool IsPushCommentMessage { get; set; }
}