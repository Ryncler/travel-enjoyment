using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace SightsService.TravelsManage;

public interface ITravelsRepository : IRepository<Travels, Guid>
{
    Task<List<Travels>> GetListByIds(List<string> ids);
}
