using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StorageService.Storage.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace StorageService.Storage;

public interface IImageAppService :
    ICrudAppService<
                ImageDto,
        Guid,
        PagedAndSortedResultRequestDto,
        ImageCreateUpdateDto,
        ImageCreateUpdateDto>
{
    Task<List<ImageDto>> GetListByLinkId(string linkId);

    Task<List<ImageDto>> CreateManyAsync(List<ImageCreateUpdateDto> input);
}