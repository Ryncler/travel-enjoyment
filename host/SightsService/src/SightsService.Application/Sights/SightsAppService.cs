using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SightsService.ActivityManage;
using SightsService.Permissions;
using SightsService.SightsManage.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Data;

namespace SightsService.SightsManage;


public class SightsAppService : CrudAppService<Sights, SightsDto, Guid, PageListAndSortedRequestDto, SightsCreateUpdateDto, SightsCreateUpdateDto>,
    ISightsAppService
{
    //protected override string GetPolicyName { get; set; } = SightsServicePermissions.Sights.Default;
    //protected override string GetListPolicyName { get; set; } = SightsServicePermissions.Sights.Default;
    //protected override string CreatePolicyName { get; set; } = SightsServicePermissions.Sights.Create;
    //protected override string UpdatePolicyName { get; set; } = SightsServicePermissions.Sights.Update;
    //protected override string DeletePolicyName { get; set; } = SightsServicePermissions.Sights.Delete;

    private readonly ISightsRepository _repository;
    private readonly IDataFilter _dataFilter;
    private readonly ISightsActivityRepository _sightsActivityRepository;

    public SightsAppService(ISightsRepository repository, ISightsActivityRepository sightsActivityRepository, IDataFilter dataFilter) : base(repository)
    {
        _repository = repository;
        _dataFilter = dataFilter;
        _sightsActivityRepository = sightsActivityRepository;
    }

    public async Task<SightsDto> GetSightsByActivityId(string id)
    {
        var sights = await _sightsActivityRepository.FindAsync(x => x.ActivityId.Equals(Guid.Parse(id)));
        if (sights == null)
            return null;

        return await GetAsync(sights.SightsId);
    }

    public async Task<List<SightsDto>> GetSightsBySearch(SightsSerachDto input)
    {
        var data = await _repository.GetListAsync(x => x.Name.Contains(input.Name) && x.Address.Contains(input.Address) && x.Ticket.Contains(input.Ticket.ToString()));
        return ObjectMapper.Map<List<Sights>, List<SightsDto>>(data);
    }


    public override async Task<PagedResultDto<SightsDto>> GetListAsync(PageListAndSortedRequestDto input)
    {
        input.Sorting = !string.IsNullOrWhiteSpace(input.Sorting) ? input.Sorting : nameof(SightsDto.CreationTime);
        if (input.IsAll)
        {
            using (_dataFilter.Disable<ISoftDelete>())
            {
                var data = await _repository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, input.Sorting);
                return new PagedResultDto<SightsDto>
                {
                    Items = ObjectMapper.Map<List<Sights>, List<SightsDto>>(data),
                    TotalCount = await _repository.GetCountAsync()
                };
            }
        }
        else
        {
            var data = await _repository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, input.Sorting);
            return new PagedResultDto<SightsDto>
            {
                Items = ObjectMapper.Map<List<Sights>, List<SightsDto>>(data),
                TotalCount = await _repository.GetCountAsync()
            };
        }
    }
}
