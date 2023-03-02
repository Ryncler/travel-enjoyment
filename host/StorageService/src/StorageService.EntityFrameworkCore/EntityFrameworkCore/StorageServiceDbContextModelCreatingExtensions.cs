using StorageService.Storage;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace StorageService.EntityFrameworkCore;

public static class StorageServiceDbContextModelCreatingExtensions
{
    public static void ConfigureStorageService(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        /* Configure all entities here. Example:

        builder.Entity<Question>(b =>
        {
            //Configure table & schema name
            b.ToTable(StorageServiceDbProperties.DbTablePrefix + "Questions", StorageServiceDbProperties.DbSchema);

            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);

            //Relations
            b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

            //Indexes
            b.HasIndex(q => q.CreationTime);
        });
        */


        builder.Entity<Image>(b =>
        {
            b.ToTable(StorageServiceDbProperties.DbTablePrefix + "Images", StorageServiceDbProperties.DbSchema);
            b.ConfigureByConvention(); 
            

            /* Configure more properties here */
        });
    }
}
