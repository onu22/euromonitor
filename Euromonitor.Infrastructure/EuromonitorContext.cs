using Microsoft.EntityFrameworkCore;
using Euromonitor.Domain.AggregateModel.BookAggregate;
using Euromonitor.Domain.AggregateModel.SubscriptionAggregate;
using Euromonitor.Domain.AggregateModel.UserAggregate;
using Euromonitor.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Euromonitor.Infrastructure
{
    public class EuromonitorContext : DbContext
    {

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<SubscriptionStatus> SubscriptionStatuses { get; set; }

        public EuromonitorContext(DbContextOptions<EuromonitorContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new BookEntityTypeConfiguration());
            builder.ApplyConfiguration(new UserEntityTypeConfiguration());
            builder.ApplyConfiguration(new SubscriptionEntityTypeConfiguration());
            builder.ApplyConfiguration(new SubscriptionStatusEntityTypeConfiguration());

        }

        public class EuromonitorContextDesignFactory : IDesignTimeDbContextFactory<EuromonitorContext>
        {
            public EuromonitorContext CreateDbContext(string[] args)
            {

                var optionsBuilder = new DbContextOptionsBuilder<EuromonitorContext>()
                    .UseSqlServer("Server=tcp:127.0.0.1,5433;Initial Catalog=euromonitordb;User Id=sa;Password=Pass@word");

                return new EuromonitorContext(optionsBuilder.Options);
            }
        }
    }
}
