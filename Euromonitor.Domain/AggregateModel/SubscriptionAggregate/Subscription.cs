using System;
using Euromonitor.Domain.SeedWork;

namespace Euromonitor.Domain.AggregateModel.SubscriptionAggregate
{

    public class Subscription : Entity, IAggregateRoot
    {
        private DateTime _subscribedDate { get; set; }
        private DateTime _unSubscribedDate { get; set; }
        private decimal _purchasePrice;

        public int SubscriptionStatusId { get; private set; }
        public int BookId { get; private set; }
        public string UserEmail { get; private set; }

        protected Subscription() { }
        public Subscription(string userEmail, int bookId, decimal purchasePrice)
        {
            UserEmail = !string.IsNullOrEmpty(userEmail) ? userEmail : throw new ArgumentNullException(nameof(userEmail));
            BookId = bookId > 0 ? bookId : throw new ArgumentNullException(nameof(bookId));      
            _purchasePrice = purchasePrice > 0 ? purchasePrice : throw new ArgumentNullException("purchase price cannot be zero or less");
            _subscribedDate = DateTime.UtcNow;
            SubscriptionStatusId = SubscriptionStatus.Active.Id;
        }

        public void UnSubscribe()
        {
            SubscriptionStatusId = SubscriptionStatus.Deactive.Id;
            _unSubscribedDate = DateTime.Now;
        }


    }
}
