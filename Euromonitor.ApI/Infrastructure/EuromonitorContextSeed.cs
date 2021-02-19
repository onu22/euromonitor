using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Euromonitor.Domain.AggregateModel.BookAggregate;
using Euromonitor.Domain.AggregateModel.SubscriptionAggregate;
using Euromonitor.Infrastructure;

namespace Euromonitor.ApI.Infrastructure
{
    public class EuromonitorContextSeed
    {


        public async Task SeedAsync(EuromonitorContext context)
        {
            using (context)
            {
                if (!context.Books.Any())
                {
                    await context.Books.AddRangeAsync(GetBooks());
                }
                if (!context.SubscriptionStatuses.Any())
                {
                    await context.SubscriptionStatuses.AddRangeAsync(GetStatuses());
                }
                await context.SaveChangesAsync();
            }
           

        }

        private IEnumerable<SubscriptionStatus> GetStatuses()
        {
            return new List<SubscriptionStatus>()
            {
                SubscriptionStatus.Active,
                SubscriptionStatus.Deactive
            };
        }

        private IEnumerable<Book> GetBooks()
        {
            return new List<Book>()
            {
                new Book("title 1","Saw you downtown singing the Blues. Watch you circle the drain. Why don't you let me stop by? Heavy is the head that wears the crown. Yes, we make angels cry, raining down on earth from up above.",100),
                new Book("title 2", "Yeah, she dances to her own beat. Oh, no. You could've been the greatest. 'Cause, baby, you're a firework. Maybe a reason why all the doors are closed. Open up your heart and just let it begin", 300),
                new Book("title 3", "Yeah, she dances to her own beat. Oh, no. You could've been the greatest. 'Cause, baby, you're a firework. Maybe a reason why all the doors are closed. Open up your heart and just let it begin", 500),
                new Book("title 4", "Yeah, she dances to her own beat. Oh, no. You could've been the greatest. 'Cause, baby, you're a firework. Maybe a reason why all the doors are closed. Open up your heart and just let it begin", 750),
            };
        }

    }
}
