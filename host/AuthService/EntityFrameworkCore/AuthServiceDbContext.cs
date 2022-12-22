using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;

namespace AuthService.EntityFrameworkCore
{
    public class AuthServiceDbContext : AbpDbContext<AuthServiceDbContext>
    {
        public AuthServiceDbContext(DbContextOptions<AuthServiceDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureOpenIddict();
        }
    }
}
