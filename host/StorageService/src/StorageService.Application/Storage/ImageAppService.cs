using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using StorageService.Permissions;
using StorageService.Storage.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace StorageService.Storage;


public class ImageAppService : CrudAppService<Image, ImageDto, Guid, PagedAndSortedResultRequestDto, ImageCreateUpdateDto, ImageCreateUpdateDto>,
    IImageAppService
{
    //protected override string GetPolicyName { get; set; } = StorageServicePermissions.Image.Default;
    //protected override string GetListPolicyName { get; set; } = StorageServicePermissions.Image.Default;
    //protected override string CreatePolicyName { get; set; } = StorageServicePermissions.Image.Create;
    //protected override string UpdatePolicyName { get; set; } = StorageServicePermissions.Image.Update;
    //protected override string DeletePolicyName { get; set; } = StorageServicePermissions.Image.Delete;

    private readonly IImageRepository _repository;

    public ImageAppService(IImageRepository repository) : base(repository)
    {
        _repository = repository;
    }

    public override async Task<ImageDto> CreateAsync(ImageCreateUpdateDto input)
    {
        if (input.IsMain)
        {
            var image = await _repository.FindAsync(x => x.LinkId.Equals(Guid.Parse(input.LinkId)) && x.IsMain == true);
            if (image != null)
                return null;
        }
        return await base.CreateAsync(input);
    }

    public async Task<List<ImageDto>> CreateManyAsync(List<ImageCreateUpdateDto> input)
    {
        var result = new List<ImageDto>();
        foreach (var item in input)
        {
            var tmp = await CreateAsync(item);
            if (tmp != null)
                result.Add(tmp);
        }
        return result;
    }

    public override async Task<ImageDto> UpdateAsync(Guid id, ImageCreateUpdateDto input)
    {
        if (input.IsMain)
        {
            var image = await _repository.FindAsync(x => x.LinkId.Equals(Guid.Parse(input.LinkId)) && x.ImageURL.Equals(input.ImageURL));
            if (image != null && image.IsMain)
                return null;
            if (image != null && !image.IsMain)
            {
                var tmp = await _repository.GetListAsync(x => x.LinkId.Equals(Guid.Parse(input.LinkId)) && x.IsMain == true);
                foreach (var item in tmp)
                {
                    await base.UpdateAsync(item.Id, new ImageCreateUpdateDto
                    {
                        ImageURL = item.ImageURL,
                        IsMain = false,
                        LinkId = item.LinkId.ToString()
                    });
                }
                return await base.UpdateAsync(id, input);
            }

        }
        return await base.UpdateAsync(id, input);

    }

    public async Task<List<ImageDto>> GetListByLinkId(string linkId)
    {
        var images = await _repository.GetListAsync(x => x.LinkId.Equals(Guid.Parse(linkId)));
        return ObjectMapper.Map<List<Image>, List<ImageDto>>(images);
    }

    public async Task<PagedResultDto<ImageDto>> GetListByLinkId(PageListByLinkIdDto input)
    {
        var linkId = Guid.Parse(input.LinkId);
        var total = (await _repository.GetListAsync(x => x.LinkId.Equals(linkId))).Count;
        var images = await _repository.GetPagedListByLinkIdAsync(linkId, input.SkipCount, input.MaxResultCount, input.Sorting);
        return new PagedResultDto<ImageDto>
        {
            Items = ObjectMapper.Map<List<Image>, List<ImageDto>>(images),
            TotalCount = total
        };
    }
}
