using CommonService.Top;
using CommonService.CategoryManage;
using CommonService.GeoManage;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore.PostgreSql;
using Volo.Abp.AuditLogging;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.PermissionManagement;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace CommonService.EntityFrameworkCore;

[DependsOn(
    typeof(CommonServiceDomainModule),
    typeof(AbpEntityFrameworkCoreModule),
    typeof(AbpPermissionManagementEntityFrameworkCoreModule),
    typeof(AbpSettingManagementEntityFrameworkCoreModule),
    typeof(AbpEntityFrameworkCorePostgreSqlModule),
    typeof(AbpAuditLoggingEntityFrameworkCoreModule),
    typeof(AbpTenantManagementEntityFrameworkCoreModule)
)]
public class CommonServiceEntityFrameworkCoreModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        AbpAuditLoggingDbProperties.DbTablePrefix = "";
        AbpCommonDbProperties.DbTablePrefix = "";
        AbpPermissionManagementDbProperties.DbTablePrefix = "";
        AbpSettingManagementDbProperties.DbTablePrefix = "";
        AbpTenantManagementDbProperties.DbTablePrefix = "";
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<CommonServiceDbContext>(options =>
        {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */
            options.AddRepository<GeoInfo, GeoInfoRepository>();
            options.AddRepository<Tag, TagRepository>();
            options.AddRepository<Category, CategoryRepository>();
            options.AddRepository<HotTop, HotTopRepository>();
        });
    }
}
