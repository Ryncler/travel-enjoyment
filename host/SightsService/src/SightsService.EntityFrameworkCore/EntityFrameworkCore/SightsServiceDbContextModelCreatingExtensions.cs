using SightsService.SightsManage;
using SightsService.CommentManage;
using SightsService.ActivityManage;
using SightsService.TravelsManage;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace SightsService.EntityFrameworkCore;

public static class SightsServiceDbContextModelCreatingExtensions
{
    public static void ConfigureSightsService(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        /* Configure all entities here. Example:

        builder.Entity<Question>(b =>
        {
            //Configure table & schema name
            b.ToTable(SightsServiceDbProperties.DbTablePrefix + "Questions", SightsServiceDbProperties.DbSchema);

            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);

            //Relations
            b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

            //Indexes
            b.HasIndex(q => q.CreationTime);
        });
        */


        builder.Entity<Sights>(b =>
        {
            b.ToTable(SightsServiceDbProperties.DbTablePrefix + "Sights", SightsServiceDbProperties.DbSchema);
            b.ConfigureByConvention();


            /* Configure more properties here */
        });


        builder.Entity<Travels>(b =>
        {
            b.ToTable(SightsServiceDbProperties.DbTablePrefix + "Travels", SightsServiceDbProperties.DbSchema);
            b.ConfigureByConvention();


            /* Configure more properties here */
        });


        builder.Entity<Activity>(b =>
        {
            b.ToTable(SightsServiceDbProperties.DbTablePrefix + "Activities", SightsServiceDbProperties.DbSchema);
            b.ConfigureByConvention();


            /* Configure more properties here */
        });


        builder.Entity<Comment>(b =>
        {
            b.ToTable(SightsServiceDbProperties.DbTablePrefix + "Comments", SightsServiceDbProperties.DbSchema);
            b.ConfigureByConvention();


            /* Configure more properties here */
        });


        builder.Entity<SightsTravels>(b =>
        {
            b.ToTable(SightsServiceDbProperties.DbTablePrefix + "SightsTravels", SightsServiceDbProperties.DbSchema);
            b.ConfigureByConvention();

            b.HasKey(e => new
            {
                e.SightsId,
                e.TravelsId,
            });

            /* Configure more properties here */
        });


        builder.Entity<SightsActivity>(b =>
        {
            b.ToTable(SightsServiceDbProperties.DbTablePrefix + "SightsActivities", SightsServiceDbProperties.DbSchema);
            b.ConfigureByConvention();

            b.HasKey(e => new
            {
                e.SightsId,
                e.ActivityId,
            });

            /* Configure more properties here */
        });


        builder.Entity<UserChoiceTravels>(b =>
        {
            b.ToTable(SightsServiceDbProperties.DbTablePrefix + "TravelsExtentions", SightsServiceDbProperties.DbSchema);
            b.ConfigureByConvention();


            /* Configure more properties here */
        });


        builder.Entity<SightsTag>(b =>
        {
            b.ToTable(SightsServiceDbProperties.DbTablePrefix + "SightsTags", SightsServiceDbProperties.DbSchema);
            b.ConfigureByConvention();

            b.HasKey(e => new
            {
                e.SightsId,
                e.TagName,
            });

            /* Configure more properties here */
        });


        builder.Entity<UserChoiceTravels>(b =>
        {
            b.ToTable(SightsServiceDbProperties.DbTablePrefix + "UserChoiceTravels", SightsServiceDbProperties.DbSchema);
            b.ConfigureByConvention(); 
            

            /* Configure more properties here */
        });
    }
}
