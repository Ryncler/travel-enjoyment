using System;
using System.Linq;
using System.Threading.Tasks;
using SightsService.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace SightsService.SightsManage;

public class SightsActivityRepository : EfCoreRepository<ISightsServiceDbContext, SightsActivity>, ISightsActivityRepository
{
    public SightsActivityRepository(IDbContextProvider<ISightsServiceDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<SightsActivity>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}