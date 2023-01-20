using System;
using System.ComponentModel;

namespace SightsService.CommentManage.Dtos;

[Serializable]
public class CommentCreateUpdateDto
{
    [DisplayName("CommentTravelsId")]
    public Guid TravelsId { get; set; }

    [DisplayName("CommentUserId")]
    public Guid UserId { get; set; }

    [DisplayName("CommentReleaseDate")]
    public DateTime ReleaseDate { get; set; }

    [DisplayName("CommentParentId")]
    public Guid? ParentId { get; set; }

    [DisplayName("CommentContent")]
    public string Content { get; set; }
}