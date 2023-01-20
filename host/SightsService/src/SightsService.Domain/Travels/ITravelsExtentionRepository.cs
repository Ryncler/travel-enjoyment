using System;
using Volo.Abp.Domain.Repositories;

namespace SightsService.TravelsManage;

public interface ITravelsExtentionRepository : IRepository<TravelsExtention, Guid>
{
}
