using System;
using System.Linq;
using System.Threading.Tasks;
using SightsService.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace SightsService.SightsManage;

public class SightsRepository : EfCoreRepository<ISightsServiceDbContext, Sights, Guid>, ISightsRepository
{
    public SightsRepository(IDbContextProvider<ISightsServiceDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<Sights>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}