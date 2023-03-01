using System;
using System.Linq;
using System.Threading.Tasks;
using SightsService.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace SightsService.SightsManage;

public class SightsTagRepository : EfCoreRepository<ISightsServiceDbContext, SightsTag>, ISightsTagRepository
{
    public SightsTagRepository(IDbContextProvider<ISightsServiceDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<SightsTag>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}