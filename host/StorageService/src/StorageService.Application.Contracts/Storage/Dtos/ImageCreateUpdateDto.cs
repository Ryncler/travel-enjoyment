using System;

namespace StorageService.Storage.Dtos;

[Serializable]
public class ImageCreateUpdateDto
{
    public string LinkId { get; set; }

    public bool IsMain { get; set; }

    public string ImageURL { get; set; }
}