using CommonService.Enum;
using System;

namespace CommonService.Top.Dtos;

[Serializable]
public class HotTopCreateUpdateDto
{
    public Guid LinkId { get; set; }

    public HotTopType TopType { get; set; }

    public int Weight { get; set; } = 1;
}