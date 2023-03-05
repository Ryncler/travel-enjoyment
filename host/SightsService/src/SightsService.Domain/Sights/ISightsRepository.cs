using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Volo.Abp.Domain.Repositories;

namespace SightsService.SightsManage;

public interface ISightsRepository : IRepository<Sights, Guid>
{
    Task<long> GetCountByCreateIdAsync(Guid createId, CancellationToken cancellationToken = default(CancellationToken));

    Task<List<Sights>> GetSightsByCreateIdAsync(Guid createId, int skipCount, int maxResultCount, string sorting, bool includeDetails = false, CancellationToken cancellationToken = default(CancellationToken));

    Task<List<Sights>> GetListByIds(List<string> ids);

    Task<List<Sights>> GetSightsBySearch(string name, string address, string ticket);
}
