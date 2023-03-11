using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace SightsService.TravelsManage;

public interface ITravelsRepository : IRepository<Travels, Guid>
{
    Task<List<Travels>> GetListByIds(List<string> ids);

    Task<List<Travels>> GetTravelsByCreateIdAsync(Guid createId, int skipCount, int maxResultCount, string sorting, bool includeDetails = false, CancellationToken cancellationToken = default(CancellationToken));
}
