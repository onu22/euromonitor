using System;
using Euromonitor.Domain.AggregateModel.BookAggregate;
using Euromonitor.Domain.AggregateModel.SubscriptionAggregate;
using Euromonitor.Domain.AggregateModel.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Euromonitor.Infrastructure.EntityConfigurations
{
    public class SubscriptionEntityTypeConfiguration : IEntityTypeConfiguration<Subscription>
    {


        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.ToTable("subscriptions");

            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).UseHiLo("subscriptionseq");

            builder.Property(o => o.UserEmail)
            .HasMaxLength(500)
            .IsRequired();

            builder.HasOne<Book>()
            .WithMany()
            .IsRequired()
            .HasForeignKey(k => k.BookId);

            builder.HasOne<SubscriptionStatus>()
            .WithMany()
            .IsRequired()
            .HasForeignKey(k => k.SubscriptionStatusId);

            builder
             .Property<DateTime>("_subscribedDate")
             .UsePropertyAccessMode(PropertyAccessMode.Field)
             .HasColumnName("SubscribedDate")
             .IsRequired();

            builder
             .Property<DateTime>("_unSubscribedDate")
             .UsePropertyAccessMode(PropertyAccessMode.Field)
             .HasColumnName("UnSubscribedDate");

            builder
            .Property<decimal>("_purchasePrice")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("PurchasePrice")
            .IsRequired();

         

        }
    }
}
