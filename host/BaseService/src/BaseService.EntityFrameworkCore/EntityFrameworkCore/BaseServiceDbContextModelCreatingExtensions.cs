using BaseService.Entities;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace BaseService.EntityFrameworkCore;

public static class BaseServiceDbContextModelCreatingExtensions
{
    public static void ConfigureBaseService(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        /* Configure all entities here. Example:

        builder.Entity<Question>(b =>
        {
            //Configure table & schema name
            b.ToTable(BaseServiceDbProperties.DbTablePrefix + "Questions", BaseServiceDbProperties.DbSchema);

            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);

            //Relations
            b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

            //Indexes
            b.HasIndex(q => q.CreationTime);
        });
        */


        builder.Entity<UserExtension>(b =>
        {
            b.ToTable(BaseServiceDbProperties.DbTablePrefix + "UserExtensions", BaseServiceDbProperties.DbSchema);
            b.ConfigureByConvention(); 
            
            b.HasKey(e => new
            {
                e.UserId,
            });

            /* Configure more properties here */
        });
    }
}
