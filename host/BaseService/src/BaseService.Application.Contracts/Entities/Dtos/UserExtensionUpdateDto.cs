using System;

namespace BaseService.Entities.Dtos;

[Serializable]
public class UserExtensionUpdateDto
{
    public string Avatar { get; set; }

    public bool Sex { get; set; }

    public string Profile { get; set; }

    public bool IsPushPrivateMessage { get; set; }

    public bool IsPushCommentMessage { get; set; }
}