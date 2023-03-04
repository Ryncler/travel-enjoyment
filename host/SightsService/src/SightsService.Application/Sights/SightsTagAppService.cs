using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SightsService.Permissions;
using SightsService.SightsManage;
using SightsService.SightsManage.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace SightsService.SightsManage;


public class SightsTagAppService : AbstractKeyCrudAppService<SightsTag, SightsTagDto, SightsTagKey, PagedAndSortedResultRequestDto, SightsTagCreateUpdateDto, SightsTagCreateUpdateDto>,
    ISightsTagAppService
{
    //protected override string GetPolicyName { get; set; } = SightsServicePermissions.SightsTag.Default;
    //protected override string GetListPolicyName { get; set; } = SightsServicePermissions.SightsTag.Default;
    //protected override string CreatePolicyName { get; set; } = SightsServicePermissions.SightsTag.Create;
    //protected override string UpdatePolicyName { get; set; } = SightsServicePermissions.SightsTag.Update;
    //protected override string DeletePolicyName { get; set; } = SightsServicePermissions.SightsTag.Delete;

    private readonly ISightsTagRepository _repository;

    public SightsTagAppService(ISightsTagRepository repository) : base(repository)
    {
        _repository = repository;
    }

    public async Task<List<SightsTagDto>> CreateManyAsync(List<SightsTagCreateUpdateDto> input)
    {
        var result = new List<SightsTagDto>();
        foreach (var item in input)
        {
            result.Add(await CreateAsync(item));
        }
        return result;
    }

    protected override Task DeleteByIdAsync(SightsTagKey id)
    {
        // TODO: AbpHelper generated
        return _repository.DeleteAsync(e =>
            e.SightsId == id.SightsId &&
            e.TagId == id.TagId
        );
    }

    protected override async Task<SightsTag> GetEntityByIdAsync(SightsTagKey id)
    {
        // TODO: AbpHelper generated
        return await AsyncExecuter.FirstOrDefaultAsync(
            (await _repository.WithDetailsAsync()).Where(e =>
                e.SightsId == id.SightsId &&
                e.TagId == id.TagId
            ));
    }

    protected override IQueryable<SightsTag> ApplyDefaultSorting(IQueryable<SightsTag> query)
    {
        // TODO: AbpHelper generated
        return query.OrderBy(e => e.SightsId);
    }

    public async Task<List<SightsTagDto>> GetAllBySightsId(string id)
    {
        var result = await _repository.GetListAsync(x => x.SightsId.Equals(Guid.Parse(id)));
        return ObjectMapper.Map<List<SightsTag>, List<SightsTagDto>>(result);
    }
}
