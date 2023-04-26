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
    public class CommonServiceBaseDbContext : AbpDbContext<CommonServiceBaseDbContext>, ICommonServiceBaseDbContext
    {
        public DbSet<AuditLogAction> AuditLogActions { get; set; }

        public CommonServiceBaseDbContext(DbContextOptions<CommonServiceBaseDbContext> options)
            : base(options)
        {

        }
    }
}
