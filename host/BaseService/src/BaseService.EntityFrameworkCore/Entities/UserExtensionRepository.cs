using System;
using System.Linq;
using System.Threading.Tasks;
using BaseService.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace BaseService.Entities;

public class UserExtensionRepository : EfCoreRepository<IBaseServiceDbContext, UserExtension>, IUserExtensionRepository
{
    public UserExtensionRepository(IDbContextProvider<IBaseServiceDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<UserExtension>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}