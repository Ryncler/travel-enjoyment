using System;
using Volo.Abp.Application.Dtos;

namespace StorageService.Storage.Dtos;

[Serializable]
public class ImageDto : FullAuditedEntityDto<Guid>
{
    public Guid LinkId { get; set; }

    public bool IsMain { get; set; }

    public string ImageURL { get; set; }
}