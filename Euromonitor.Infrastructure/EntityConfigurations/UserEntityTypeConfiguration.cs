using System;
using Euromonitor.Domain.AggregateModel.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Euromonitor.Infrastructure.EntityConfigurations
{

    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id)
                .UseHiLo("userseq");

            builder.Property(o => o.Email) .IsRequired();

        }
    }

}
