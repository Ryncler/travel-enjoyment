using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SightsService.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace SightsService.TravelsManage;

public class TravelsRepository : EfCoreRepository<ISightsServiceDbContext, Travels, Guid>, ITravelsRepository
{
    public TravelsRepository(IDbContextProvider<ISightsServiceDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<Travels>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }

    public async Task<List<Travels>> GetListByIds(List<string> ids)
    {
        var db = await GetDbContextAsync();

        var result = ids.Select(item =>
        {
            return db.Travels.Where(x => x.Id.Equals(Guid.Parse(item))).FirstOrDefault();
        }).ToList();
        return result;
    }

    public async Task<List<Travels>> GetTravelsByCreateIdAsync(Guid createId, int skipCount, int maxResultCount, string sorting, bool includeDetails = false, CancellationToken cancellationToken = default(CancellationToken))
    {
        var query = ((!includeDetails) ? (await GetDbSetAsync().ConfigureAwait(continueOnCapturedContext: false)) : (await WithDetailsAsync().ConfigureAwait(continueOnCapturedContext: false)));
        var list = await query.Where(x => x.CreatorId.Equals(createId)).OrderByIf<Travels, IQueryable<Travels>>(!sorting.IsNullOrWhiteSpace(), sorting).PageBy(skipCount, maxResultCount).ToListAsync(GetCancellationToken(cancellationToken))
                .ConfigureAwait(continueOnCapturedContext: false);
        return list;
    }
}