using System;
using Volo.Abp.Domain.Repositories;

namespace StorageService.Storage;

public interface IImageRepository : IRepository<Image, Guid>
{
}
