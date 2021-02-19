using System.Collections.Generic;
using System.Threading.Tasks;
using Euromonitor.Domain.SeedWork;

namespace Euromonitor.Domain.AggregateModel.SubscriptionAggregate
{
    public interface ISubscriptionRepository : IRepository<Subscription>
    {
        Task Save(Subscription subscription);
        Task<Subscription> FinByUserBookAsync(string email, int bookId);
        Task<IEnumerable<Subscription>> FindByUserActivesAsync(string userEmail);
        Task Update(Subscription subscription);

    }
}
