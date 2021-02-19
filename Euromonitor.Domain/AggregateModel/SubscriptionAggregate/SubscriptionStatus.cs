
namespace Euromonitor.Domain.AggregateModel.SubscriptionAggregate
{

    public class SubscriptionStatus
    {
        protected SubscriptionStatus(){ }
        public int Id { get; private set; }
        public string Name { get; private set; }

        public static SubscriptionStatus Active = new SubscriptionStatus(1, nameof(Active));
        public static SubscriptionStatus Deactive = new SubscriptionStatus(2, nameof(Deactive));

        public SubscriptionStatus(int id, string name)
        {
            Id = id;
            Name = name;
        }

    };
}
