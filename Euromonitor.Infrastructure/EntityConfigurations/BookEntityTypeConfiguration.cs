using System;
using Euromonitor.Domain.AggregateModel.BookAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Euromonitor.Infrastructure.EntityConfigurations
{
    public class BookEntityTypeConfiguration : IEntityTypeConfiguration<Book>
    {      
       public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("books");

            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id)
                .UseHiLo("bookseq");

            builder.Property(o => o.Name)
                 .HasMaxLength(100)
                 .IsRequired();

            builder.Property(o => o.Text)
                .IsRequired();

            builder.Property(o => o.Price)
                .IsRequired();

        }
    }
}
