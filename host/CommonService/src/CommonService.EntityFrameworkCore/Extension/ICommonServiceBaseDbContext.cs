using CommonService.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AuditLogging;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace CommonService.Extension
{
    [ConnectionStringName(CommonServiceDbProperties.BaseConnectionStringName)]
    public interface ICommonServiceBaseDbContext : IEfCoreDbContext
    {
        DbSet<AuditLogAction> AuditLogActions { get; set; }
    }
}
