using System;
using System.Linq;
using System.Threading.Tasks;
using SightsService.Permissions;
using SightsService.SightsManage.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace SightsService.SightsManage;


public class SightsTravelsAppService : AbstractKeyCrudAppService<SightsTravels, SightsTravelsDto, SightsTravelsKey, PageListAndSortedRequestDto, SightsTravelsCreateUpdateDto, SightsTravelsCreateUpdateDto>,
    ISightsTravelsAppService
{
    //protected override string GetPolicyName { get; set; } = SightsServicePermissions.SightsTravels.Default;
    //protected override string GetListPolicyName { get; set; } = SightsServicePermissions.SightsTravels.Default;
    //protected override string CreatePolicyName { get; set; } = SightsServicePermissions.SightsTravels.Create;
    //protected override string UpdatePolicyName { get; set; } = SightsServicePermissions.SightsTravels.Update;
    //protected override string DeletePolicyName { get; set; } = SightsServicePermissions.SightsTravels.Delete;

    private readonly ISightsTravelsRepository _repository;

    public SightsTravelsAppService(ISightsTravelsRepository repository) : base(repository)
    {
        _repository = repository;
    }

    protected override Task DeleteByIdAsync(SightsTravelsKey id)
    {
        // TODO: AbpHelper generated
        return _repository.DeleteAsync(e =>
            e.SightsId == id.SightsId &&
            e.TravelsId == id.TravelsId
        );
    }

    protected override async Task<SightsTravels> GetEntityByIdAsync(SightsTravelsKey id)
    {
        // TODO: AbpHelper generated
        return await AsyncExecuter.FirstOrDefaultAsync(
            (await _repository.WithDetailsAsync()).Where(e =>
                e.SightsId == id.SightsId &&
                e.TravelsId == id.TravelsId
            ));
    }

    protected override IQueryable<SightsTravels> ApplyDefaultSorting(IQueryable<SightsTravels> query)
    {
        // TODO: AbpHelper generated
        return query.OrderBy(e => e.SightsId);
    }

}
