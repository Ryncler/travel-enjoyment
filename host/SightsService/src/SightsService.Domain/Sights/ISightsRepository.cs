using System;
using Volo.Abp.Domain.Repositories;

namespace SightsService.SightsManage;

public interface ISightsRepository : IRepository<Sights, Guid>
{
}
