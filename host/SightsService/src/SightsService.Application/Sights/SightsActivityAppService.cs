using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SightsService.Permissions;
using SightsService.SightsManage.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace SightsService.SightsManage;


public class SightsActivityAppService : AbstractKeyCrudAppService<SightsActivity, SightsActivityDto, SightsActivityKey, PageListAndSortedRequestDto, SightsActivityCreateUpdateDto, SightsActivityCreateUpdateDto>,
    ISightsActivityAppService
{
    //protected override string GetPolicyName { get; set; } = SightsServicePermissions.SightsActivity.Default;
    //protected override string GetListPolicyName { get; set; } = SightsServicePermissions.SightsActivity.Default;
    //protected override string CreatePolicyName { get; set; } = SightsServicePermissions.SightsActivity.Create;
    //protected override string UpdatePolicyName { get; set; } = SightsServicePermissions.SightsActivity.Update;
    //protected override string DeletePolicyName { get; set; } = SightsServicePermissions.SightsActivity.Delete;

    private readonly ISightsActivityRepository _repository;

    public SightsActivityAppService(ISightsActivityRepository repository) : base(repository)
    {
        _repository = repository;
    }

    protected override Task DeleteByIdAsync(SightsActivityKey id)
    {
        // TODO: AbpHelper generated
        return _repository.DeleteAsync(e =>
            e.SightsId == id.SightsId &&
            e.ActivityId == id.ActivityId
        );
    }

    protected override async Task<SightsActivity> GetEntityByIdAsync(SightsActivityKey id)
    {
        // TODO: AbpHelper generated
        return await AsyncExecuter.FirstOrDefaultAsync(
            (await _repository.WithDetailsAsync()).Where(e =>
                e.SightsId == id.SightsId &&
                e.ActivityId == id.ActivityId
            ));
    }

    protected override IQueryable<SightsActivity> ApplyDefaultSorting(IQueryable<SightsActivity> query)
    {
        // TODO: AbpHelper generated
        return query.OrderBy(e => e.SightsId);
    }


    public async Task<List<SightsActivityDto>> GetAllBySightsId(string id)
    {
        var result = await _repository.GetListAsync(x => x.SightsId.Equals(Guid.Parse(id)));
        return ObjectMapper.Map<List<SightsActivity>, List<SightsActivityDto>>(result);
    }
}
