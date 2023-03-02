using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SightsService.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace SightsService.ActivityManage;

public class ActivityRepository : EfCoreRepository<ISightsServiceDbContext, Activity, Guid>, IActivityRepository
{
    public ActivityRepository(IDbContextProvider<ISightsServiceDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<Activity>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }

    public async Task<List<Activity>> GetActivityByCreateIdAsync(Guid createId, int skipCount, int maxResultCount, string sorting, bool includeDetails = false, CancellationToken cancellationToken = default(CancellationToken))
    {
        var query = ((!includeDetails) ? (await GetDbSetAsync().ConfigureAwait(continueOnCapturedContext: false)) : (await WithDetailsAsync().ConfigureAwait(continueOnCapturedContext: false)));
        var list = await query.Where(x => x.CreatorId.Equals(createId)).OrderByIf<Activity, IQueryable<Activity>>(!sorting.IsNullOrWhiteSpace(), sorting).PageBy(skipCount, maxResultCount).ToListAsync(GetCancellationToken(cancellationToken))
                .ConfigureAwait(continueOnCapturedContext: false);
        return list;
    }

    public async Task<long> GetCountByCreateIdAsync(Guid createId, CancellationToken cancellationToken = default(CancellationToken))
    {
        var query = (await GetDbSetAsync().ConfigureAwait(continueOnCapturedContext: false));
        return query.Where(x => x.CreatorId.Equals(createId)).Count();
    }

    public async Task<List<Activity>> GetListByIds(List<string> ids)
    {
        var db = await GetDbContextAsync();

        var result = ids.Select(item =>
        {
            return db.Activities.Where(x => x.Id.Equals(Guid.Parse(item))).FirstOrDefault();
        }).ToList();
        return result;
    }
}