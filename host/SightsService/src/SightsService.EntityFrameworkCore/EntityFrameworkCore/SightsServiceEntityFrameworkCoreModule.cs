using SightsService.SightsManage;
using SightsService.CommentManage;
using SightsService.ActivityManage;
using SightsService.TravelsManage;
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

namespace SightsService.EntityFrameworkCore;

[DependsOn(
    typeof(SightsServiceDomainModule),
    typeof(AbpEntityFrameworkCoreModule),
    typeof(AbpPermissionManagementEntityFrameworkCoreModule),
    typeof(AbpSettingManagementEntityFrameworkCoreModule),
    typeof(AbpEntityFrameworkCorePostgreSqlModule),
    typeof(AbpAuditLoggingEntityFrameworkCoreModule),
    typeof(AbpTenantManagementEntityFrameworkCoreModule)
)]
public class SightsServiceEntityFrameworkCoreModule : AbpModule
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
        context.Services.AddAbpDbContext<SightsServiceDbContext>(options =>
        {
            /* Add custom repositories here. Example:
             * options.AddRepository<Question, EfCoreQuestionRepository>();
             */
            options.AddRepository<Sights, SightsRepository>();
            options.AddRepository<Travels, TravelsRepository>();
            options.AddRepository<Activity, ActivityRepository>();
            options.AddRepository<Comment, CommentRepository>();
            options.AddRepository<SightsTravels, SightsTravelsRepository>();
            options.AddRepository<SightsActivity, SightsActivityRepository>();
            options.AddRepository<SightsTag, SightsTagRepository>();
            options.AddRepository<UserChoiceTravels, UserChoiceTravelsRepository>();
        });
    }
}
