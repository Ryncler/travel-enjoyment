using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Volo.Abp.Domain.Repositories;

namespace StorageService.Storage;

public interface IImageRepository : IRepository<Image, Guid>
{
    Task<List<Image>> GetPagedListByLinkIdAsync(Guid linkId, int skipCount, int maxResultCount, string sorting, bool includeDetails = false, CancellationToken cancellationToken = default);
}
