using CommonService.User;
using CommonService.Top;
using CommonService.CategoryManage;
using CommonService.GeoManage;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace CommonService.EntityFrameworkCore;

public static class CommonServiceDbContextModelCreatingExtensions
{
    public static void ConfigureCommonService(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        /* Configure all entities here. Example:

        builder.Entity<Question>(b =>
        {
            //Configure table & schema name
            b.ToTable(CommonServiceDbProperties.DbTablePrefix + "Questions", CommonServiceDbProperties.DbSchema);

            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);

            //Relations
            b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

            //Indexes
            b.HasIndex(q => q.CreationTime);
        });
        */


        builder.Entity<GeoInfo>(b =>
        {
            b.ToTable(CommonServiceDbProperties.DbTablePrefix + "GeoInfos", CommonServiceDbProperties.DbSchema);
            b.ConfigureByConvention(); 
            

            /* Configure more properties here */
        });


        builder.Entity<Tag>(b =>
        {
            b.ToTable(CommonServiceDbProperties.DbTablePrefix + "Tags", CommonServiceDbProperties.DbSchema);
            b.ConfigureByConvention(); 
            

            /* Configure more properties here */
        });


        builder.Entity<Category>(b =>
        {
            b.ToTable(CommonServiceDbProperties.DbTablePrefix + "Categories", CommonServiceDbProperties.DbSchema);
            b.ConfigureByConvention(); 
            

            /* Configure more properties here */
        });


        builder.Entity<HotTop>(b =>
        {
            b.ToTable(CommonServiceDbProperties.DbTablePrefix + "HotTops", CommonServiceDbProperties.DbSchema);
            b.ConfigureByConvention(); 
            

            /* Configure more properties here */
        });


        builder.Entity<UserTravel>(b =>
        {
            b.ToTable(CommonServiceDbProperties.DbTablePrefix + "UserTravels", CommonServiceDbProperties.DbSchema);
            b.ConfigureByConvention(); 
            

            /* Configure more properties here */
        });
    }
}
