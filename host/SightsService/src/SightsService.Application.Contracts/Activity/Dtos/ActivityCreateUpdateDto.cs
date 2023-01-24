using System;
using System.ComponentModel;

namespace SightsService.ActivityManage.Dtos;

[Serializable]
public class ActivityCreateUpdateDto
{
    [DisplayName("ActivityName")]
    public string Name { get; set; }

    [DisplayName("ActivityContent")]
    public string Content { get; set; }
}