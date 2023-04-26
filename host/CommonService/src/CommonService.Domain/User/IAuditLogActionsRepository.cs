using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.AuditLogging;
using Volo.Abp.Domain.Repositories;

namespace CommonService.User
{
    public interface IAuditLogActionsRepository : IRepository<AuditLogAction, Guid>
    {
    }
}
