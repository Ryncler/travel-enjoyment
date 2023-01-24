using System;
using System.Linq;
using System.Threading.Tasks;
using CommonService.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace CommonService.GeoManage;

public class GeoInfoRepository : EfCoreRepository<ICommonServiceDbContext, GeoInfo, Guid>, IGeoInfoRepository
{
    public GeoInfoRepository(IDbContextProvider<ICommonServiceDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<GeoInfo>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}