using System;
using SightsService.ActivityManage.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using SightsService.Permissions;
using SightsService.TravelsManage.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using SightsService.SightsManage.Dtos;
using Volo.Abp;

namespace SightsService.TravelsManage;


public class TravelsAppService : CrudAppService<Travels, TravelsDto, Guid, PageListAndSortedRequestDto, TravelsCreateUpdateDto, TravelsCreateUpdateDto>,
    ITravelsAppService
{
    //protected override string GetPolicyName { get; set; } = SightsServicePermissions.Travels.Default;
    //protected override string GetListPolicyName { get; set; } = SightsServicePermissions.Travels.Default;
    //protected override string CreatePolicyName { get; set; } = SightsServicePermissions.Travels.Create;
    //protected override string UpdatePolicyName { get; set; } = SightsServicePermissions.Travels.Update;
    //protected override string DeletePolicyName { get; set; } = SightsServicePermissions.Travels.Delete;

    private readonly ITravelsRepository _repository;


    public TravelsAppService(ITravelsRepository repository) : base(repository)
    {
        _repository = repository;
    }

    public async Task<List<TravelsDto>> GetActivityListByIdsAsync(List<string> ids)
    {
        var travels = await _repository.GetListByIds(ids);
        return ObjectMapper.Map<List<Travels>, List<TravelsDto>>(travels);
    }

    public async Task<int> GetCountByUserId(string id)
    {
        var result = await _repository.GetListAsync(x => x.CreatorId.Equals(Guid.Parse(id)));
        return result.Count;
    }

    public async Task<PagedResultDto<TravelsDto>> GetPagedByCreateIdAsync(Guid createId, PageListAndSortedRequestDto input)
    {
        if (createId.Equals(Guid.Empty))
            return new PagedResultDto<TravelsDto>
            {
                Items = null,
                TotalCount = 0
            };

        input.Sorting = !string.IsNullOrWhiteSpace(input.Sorting) ? input.Sorting : nameof(SightsDto.CreationTime);
        var data = await _repository.GetTravelsByCreateIdAsync(createId, input.SkipCount, input.MaxResultCount, input.Sorting);
        return new PagedResultDto<TravelsDto>
        {
            Items = ObjectMapper.Map<List<Travels>, List<TravelsDto>>(data),
            TotalCount = await GetCountByUserId(createId.ToString())
        };

    }
}
