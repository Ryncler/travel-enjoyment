using CommonService.EntityFrameworkCore;
using CommonService.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AuditLogging;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace CommonService.User
{
    public class AuditLogActionsRepository : EfCoreRepository<ICommonServiceBaseDbContext, AuditLogAction, Guid>, IAuditLogActionsRepository
    {
        public AuditLogActionsRepository(IDbContextProvider<ICommonServiceBaseDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }
    }
}
