using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SightsService.Permissions;
using SightsService.TravelsManage.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace SightsService.TravelsManage;


public class UserChoiceTravelsAppService : CrudAppService<UserChoiceTravels, UserChoiceTravelsDto, Guid, PagedAndSortedResultRequestDto, UserChoiceTravelsCreateUpdateDto, UserChoiceTravelsCreateUpdateDto>,
    IUserChoiceTravelsAppService
{
    //protected override string GetPolicyName { get; set; } = SightsServicePermissions.UserChoiceTravels.Default;
    //protected override string GetListPolicyName { get; set; } = SightsServicePermissions.UserChoiceTravels.Default;
    //protected override string CreatePolicyName { get; set; } = SightsServicePermissions.UserChoiceTravels.Create;
    //protected override string UpdatePolicyName { get; set; } = SightsServicePermissions.UserChoiceTravels.Update;
    //protected override string DeletePolicyName { get; set; } = SightsServicePermissions.UserChoiceTravels.Delete;

    private readonly IUserChoiceTravelsRepository _repository;

    private readonly ITravelsRepository _travelsRepository;

    public UserChoiceTravelsAppService(IUserChoiceTravelsRepository repository, ITravelsRepository travelsRepository) : base(repository)
    {
        _repository = repository;
        _travelsRepository = travelsRepository;
    }

    public override async Task<UserChoiceTravelsDto> CreateAsync(UserChoiceTravelsCreateUpdateDto input)
    {
        var userChoiceTravel = await _repository.FindAsync(x => x.CreatorId.Equals(CurrentUser.Id));
        if (userChoiceTravel != null)
            return null;
        return await base.CreateAsync(input);
    }
    public async Task<List<TravelsDto>> GetChoiceTravel(string id)
    {
        var result = new List<TravelsDto>();
        var userChoiceTravel = await _repository.FindAsync(x => x.CreatorId.Equals(Guid.Parse(id)));
        if (userChoiceTravel != null)
        {
            var travel1 = await _travelsRepository.FindAsync(x => x.Id.Equals(userChoiceTravel.OneTravelsId));
            var travel2 = await _travelsRepository.FindAsync(x => x.Id.Equals(userChoiceTravel.TwoTravelsId));
            if (travel1 != null && travel2 != null)
            {
                result.Add(ObjectMapper.Map<Travels, TravelsDto>(travel1));
                result.Add(ObjectMapper.Map<Travels, TravelsDto>(travel2));
            }
        }
        return result;

    }
}
