using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommonService.Permissions;
using CommonService.User.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CommonService.User;


public class UserTravelAppService : CrudAppService<UserTravel, UserTravelDto, Guid, PagedAndSortedResultRequestDto, UserTravelCreateUpdateDto, UserTravelCreateUpdateDto>,
    IUserTravelAppService
{
    //protected override string GetPolicyName { get; set; } = CommonServicePermissions.UserTravel.Default;
    //protected override string GetListPolicyName { get; set; } = CommonServicePermissions.UserTravel.Default;
    //protected override string CreatePolicyName { get; set; } = CommonServicePermissions.UserTravel.Create;
    //protected override string UpdatePolicyName { get; set; } = CommonServicePermissions.UserTravel.Update;
    //protected override string DeletePolicyName { get; set; } = CommonServicePermissions.UserTravel.Delete;

    private readonly IUserTravelRepository _repository;

    public UserTravelAppService(IUserTravelRepository repository) : base(repository)
    {
        _repository = repository;
    }

    public async Task<List<UserTravelDto>> GetListByUserId(string id)
    {
        var result = await _repository.GetListAsync(x => x.UserId.Equals(Guid.Parse(id)));
        return ObjectMapper.Map<List<UserTravel>, List<UserTravelDto>>(result);
    }

}
