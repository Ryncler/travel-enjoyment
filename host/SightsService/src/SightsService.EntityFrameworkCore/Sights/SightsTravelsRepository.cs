using System;
using System.Linq;
using System.Threading.Tasks;
using SightsService.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace SightsService.SightsManage;

public class SightsTravelsRepository : EfCoreRepository<ISightsServiceDbContext, SightsTravels>, ISightsTravelsRepository
{
    public SightsTravelsRepository(IDbContextProvider<ISightsServiceDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<SightsTravels>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}