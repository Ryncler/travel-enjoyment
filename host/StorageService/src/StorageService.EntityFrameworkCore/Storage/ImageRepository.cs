using System;
using System.Linq;
using System.Threading.Tasks;
using StorageService.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace StorageService.Storage;

public class ImageRepository : EfCoreRepository<IStorageServiceDbContext, Image, Guid>, IImageRepository
{
    public ImageRepository(IDbContextProvider<IStorageServiceDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<Image>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}