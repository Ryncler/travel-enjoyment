using System;
using SightsService.TravelsManage;
using Volo.Abp.Domain.Repositories;

namespace SightsService.TravelsManage;

public interface IUserChoiceTravelsRepository : IRepository<UserChoiceTravels, Guid>
{
}
