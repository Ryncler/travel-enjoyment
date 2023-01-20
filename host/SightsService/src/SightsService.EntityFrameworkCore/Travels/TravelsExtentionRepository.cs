using System;
using System.Linq;
using System.Threading.Tasks;
using SightsService.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace SightsService.TravelsManage;

public class TravelsExtentionRepository : EfCoreRepository<ISightsServiceDbContext, TravelsExtention, Guid>, ITravelsExtentionRepository
{
    public TravelsExtentionRepository(IDbContextProvider<ISightsServiceDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<TravelsExtention>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}