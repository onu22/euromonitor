using Euromonitor.Domain.AggregateModel.SubscriptionAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Euromonitor.Infrastructure.EntityConfigurations
{
    public class SubscriptionStatusEntityTypeConfiguration : IEntityTypeConfiguration<SubscriptionStatus>
    {
       

        public void Configure(EntityTypeBuilder<SubscriptionStatus> builder)
        {
            builder.ToTable("subscriptionStatus");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
               .HasDefaultValue(1)
               .ValueGeneratedNever()
               .IsRequired();

             builder.Property(o => o.Name)
                  .HasMaxLength(50)
                  .IsRequired();


        }
    }
}
