using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Volo.Abp.Domain.Repositories;

namespace SightsService.ActivityManage;

public interface IActivityRepository : IRepository<Activity, Guid>
{
    Task<List<Activity>> GetActivityByCreateIdAsync(Guid createId, int skipCount, int maxResultCount, string sorting, bool includeDetails = false, CancellationToken cancellationToken = default(CancellationToken));

    Task<long> GetCountByCreateIdAsync(Guid createId, CancellationToken cancellationToken = default(CancellationToken));

    Task<List<Activity>> GetListByIdsAsync(List<string> ids);
}
