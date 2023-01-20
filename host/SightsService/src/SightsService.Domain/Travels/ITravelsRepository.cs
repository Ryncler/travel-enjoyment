using System;
using Volo.Abp.Domain.Repositories;

namespace SightsService.TravelsManage;

public interface ITravelsRepository : IRepository<Travels, Guid>
{
}
