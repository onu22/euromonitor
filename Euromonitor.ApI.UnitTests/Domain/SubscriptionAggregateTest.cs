using System;
using Euromonitor.Domain.AggregateModel.SubscriptionAggregate;
using Xunit;


namespace Euromonitor.ApI.UnitTests.Domain
{
    public class SubscriptionAggregateTest
    {
        public SubscriptionAggregateTest() {}


        [Fact]
        public void UnSubscribe_Sets_Status()
        {

            //Act
            var subscription = new Subscription("onuorahpascal@yahoo.com", 1, 150);
            subscription.UnSubscribe();
            //Assert
            Assert.Equal(subscription.SubscriptionStatusId, SubscriptionStatus.Deactive.Id);
        }

        [Fact]
        public void Create_Subscription_fail()
        {
            string email = string.Empty;
            int bookId = 0;
            decimal purchasePrice = 100;

            //Act - Assert
            Assert.Throws<ArgumentNullException>(() => new Subscription(email, bookId,purchasePrice));
        }

        [Fact]
        public void Create_Subscription_success()
        {

            string email = "onuorahpascal@yahoo.com";
            int bookId = 1;
            decimal purchasePrice = 100;

            //Act
            var subscription = new Subscription(email, bookId,purchasePrice);
            //Assert
            Assert.NotNull(subscription);
        }

    }
}
