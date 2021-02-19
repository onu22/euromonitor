using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Euromonitor.Domain.AggregateModel.SubscriptionAggregate;
using Microsoft.EntityFrameworkCore;

namespace Euromonitor.Infrastructure.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly EuromonitorContext _context;
        public SubscriptionRepository(EuromonitorContext context)
        {
            _context = context;
        }

        public async Task Save(Subscription subscription)
        {
            _context.Subscriptions.Add(subscription);
            await _context.SaveChangesAsync();
        }

        public async Task<Subscription> FinByUserBookAsync(string userEmail, int bookId)
        {
          
            return await _context.Subscriptions
                        .FirstOrDefaultAsync(s => s.UserEmail == userEmail && s.BookId == bookId);
        }


        public async Task<IEnumerable<Subscription>> FindByUserActivesAsync(string userEmail)
        {
           var query = _context.Subscriptions
                .Where(s => s.UserEmail == userEmail && s.SubscriptionStatusId == SubscriptionStatus.Active.Id);

            return await query.ToListAsync();

        }

        public async Task Update(Subscription subscription)
        {
            _context.Update(subscription);
            await _context.SaveChangesAsync();
           
        }
    }
}
