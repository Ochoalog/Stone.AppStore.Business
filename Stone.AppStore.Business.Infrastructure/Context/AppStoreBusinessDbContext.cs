using Microsoft.EntityFrameworkCore;
using Stone.AppStore.Business.Domain;
using Stone.AppStore.Business.Domain.Entities;

namespace Stone.AppStore.Business.Infrastructure.Context
{
    public class AppStoreBusinessDbContext : DbContext
    {
        public AppStoreBusinessDbContext(DbContextOptions<AppStoreBusinessDbContext> options)
           : base(options)
        { }

        public DbSet<Payment> Payments { get; set; }

        public DbSet<CreditCard> CreditCards { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(AppStoreBusinessDbContext).Assembly);
        }
    }
}
