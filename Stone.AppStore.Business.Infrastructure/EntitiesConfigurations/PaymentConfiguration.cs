using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stone.AppStore.Business.Domain.Entities;

namespace Stone.AppStore.Business.Infrastructure.EntitiesConfigurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(c => c.Id);
        }
    }
}
