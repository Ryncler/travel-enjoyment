using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

    public async Task<List<Image>> GetPagedListByLinkIdAsync(Guid linkId, int skipCount, int maxResultCount, string sorting, bool includeDetails = false, CancellationToken cancellationToken = default)
    {
        var queryable = includeDetails ? await WithDetailsAsync() : await GetDbSetAsync();
        return await queryable.Where(x => x.LinkId.Equals(linkId)).OrderByIf<Image, IQueryable<Image>>(!sorting.IsNullOrWhiteSpace(), sorting).PageBy(skipCount, maxResultCount)
            .ToListAsync(GetCancellationToken(cancellationToken));
    }
}