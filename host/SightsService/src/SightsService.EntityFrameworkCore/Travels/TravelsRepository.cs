using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
}