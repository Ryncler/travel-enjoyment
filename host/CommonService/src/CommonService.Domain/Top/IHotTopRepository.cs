using System;
using Volo.Abp.Domain.Repositories;

namespace CommonService.Top;

public interface IHotTopRepository : IRepository<HotTop, Guid>
{
}
