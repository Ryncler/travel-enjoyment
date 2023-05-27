using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using CommonService.Permissions;
using CommonService.User.Dtos;
using CommonService.UserManage.Dtos;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
namespace CommonService.User;


public class UserTravelAppService : CrudAppService<UserTravel, UserTravelDto, Guid, PagedAndSortedResultRequestDto, UserTravelCreateUpdateDto, UserTravelCreateUpdateDto>,
    IUserTravelAppService
{
    protected override string GetPolicyName { get; set; } = CommonServicePermissions.UserTravel.Default;
    protected override string GetListPolicyName { get; set; } = CommonServicePermissions.UserTravel.Default;
    protected override string CreatePolicyName { get; set; } = CommonServicePermissions.UserTravel.Create;
    protected override string UpdatePolicyName { get; set; } = CommonServicePermissions.UserTravel.Update;
    protected override string DeletePolicyName { get; set; } = CommonServicePermissions.UserTravel.Delete;

    private readonly IUserTravelRepository _repository;

    public UserTravelAppService(IUserTravelRepository repository) : base(repository)
    {
        _repository = repository;
    }

    [Authorize(CommonServicePermissions.UserTravel.Default)]
    public async Task<List<UserTravelDto>> GetListByUserIdAsync(PagedUserTravelsByUserDto input)
    {
        var userTravelQuery = await _repository.GetQueryableAsync();
        var userTravels = userTravelQuery.Where(x => x.UserId.Equals(input.UserId))
             .OrderByDescending(x => x.CreationTime)
             .PageBy(input.SkipCount, input.MaxResultCount)
             .ToList();
        return ObjectMapper.Map<List<UserTravel>, List<UserTravelDto>>(userTravels);
    }

    public override async Task<UserTravelDto> CreateAsync(UserTravelCreateUpdateDto input)
    {
        var userTravel = await _repository.FindAsync(x => x.UserId.Equals(input.UserId) && x.TravelId.Equals(input.TravelId) && !x.IsDeleted);
        if (userTravel == null)
            return await base.CreateAsync(input);

        return null;
    }

    [Authorize(CommonServicePermissions.UserTravel.Default)]
    public async Task<List<UserTravelDto>> GetListByTravelIdAsync(string id)
    {
        var result = await _repository.GetListAsync(x => x.TravelId.Equals(Guid.Parse(id)));
        return ObjectMapper.Map<List<UserTravel>, List<UserTravelDto>>(result);
    }

    //[Authorize(CommonServicePermissions.UserTravel.Default)]
    public async Task<int> GetCountByUserIdAsync(string id)
    {
        var result = await _repository.GetListAsync(x => x.UserId.Equals(Guid.Parse(id)));
        return result.Count;
    }

    //[Authorize(CommonServicePermissions.UserTravel.Default)]
    public async Task<int> GetCountByTravelIdAsync(string id)
    {
        var result = await _repository.GetListAsync(x => x.TravelId.Equals(Guid.Parse(id)));
        return result.Count;
    }

    [Authorize(CommonServicePermissions.UserTravel.Delete)]
    public async Task DeleteByTravelIdAndUserIdAsync(string userId, string travelId)
    {
        var userTravel = await _repository.FindAsync(x => x.TravelId.Equals(Guid.Parse(travelId)) && x.UserId.Equals(Guid.Parse(userId)));
        if (userTravel != null)
            await DeleteAsync(userTravel.Id);
    }

    [Authorize(CommonServicePermissions.UserTravel.Default)]
    public async Task<bool> ExitsUserTravelAsync(string userId, string travelId)
    {
        var userTravel = await _repository.FindAsync(x => x.TravelId.Equals(Guid.Parse(travelId)) && x.UserId.Equals(Guid.Parse(userId)));
        if (userTravel != null)
            return true;
        return false;
    }
}
