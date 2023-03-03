using System;
using Volo.Abp.Domain.Repositories;

namespace CommonService.User;

public interface IUserTravelRepository : IRepository<UserTravel, Guid>
{
}
