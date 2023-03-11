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

    private readonly ITravelsExtentionRepository _travelsExtentionRepository;

    private readonly ITravelsExtentionAppService _travelsExtentionAppService;

    public TravelsAppService(ITravelsRepository repository, ITravelsExtentionRepository travelsExtentionRepository, ITravelsExtentionAppService travelsExtentionAppService) : base(repository)
    {
        _repository = repository;
        _travelsExtentionRepository = travelsExtentionRepository;
        _travelsExtentionAppService = travelsExtentionAppService;
    }

    public override async Task<TravelsDto> CreateAsync(TravelsCreateUpdateDto input)
    {
        var result = await base.CreateAsync(input);

        if (result != null)
        {
            await _travelsExtentionAppService.CreateAsync(new TravelsExtentionCreateUpdateDto
            {
                TravelsId = result.Id,
                StarValue = 1,
                CalorificValue = 1
            });
        }
        return result;
    }

    public async Task<List<TravelsDto>> GetActivityListByIdsAsync(List<string> ids)
    {
        var travels = await _repository.GetListByIds(ids);
        return ObjectMapper.Map<List<Travels>, List<TravelsDto>>(travels);
    }

    public async Task<TravelsExtentionDto> GetExtentionByIdAsync(string id)
    {
        var travel = await _travelsExtentionRepository.GetAsync(x => x.TravelsId.Equals(Guid.Parse(id)));
        if (travel != null)
            return ObjectMapper.Map<TravelsExtention, TravelsExtentionDto>(travel);

        return null;
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
