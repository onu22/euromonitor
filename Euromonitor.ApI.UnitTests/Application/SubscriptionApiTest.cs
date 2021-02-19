
using System.Threading.Tasks;
using Euromonitor.ApI.Controllers;
using Euromonitor.ApI.Models;
using Euromonitor.Domain.AggregateModel.BookAggregate;
using Euromonitor.Domain.AggregateModel.SubscriptionAggregate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

using Moq;
using Xunit;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Euromonitor.ApI.UnitTests.Application
{
    public class SubscriptionApiTest
    {
        private readonly Mock<ILogger<SubscriptionController>> _loggerMock;
        private readonly Mock<IBookRepository> _bookRepository;
        private readonly Mock<ISubscriptionRepository> _subscriptionRepository;


        public SubscriptionApiTest()
        {
            _loggerMock = new Mock<ILogger<SubscriptionController>>();
            _bookRepository = new Mock<IBookRepository>();
            _subscriptionRepository = new Mock<ISubscriptionRepository>();
        }


        [Fact]
        public async Task Subscribe_success()
        {
            //Arrange
            IEnumerable<Book> fakeBook = new List<Book>() { new Book("Professional C#", "sample text", 100), new Book("Professional Angular", "sample text", 120) };

            _bookRepository.Setup(x => x.FindByIdsAsync(It.IsAny<int[]>())).Returns(Task.FromResult(fakeBook));

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {new Claim(ClaimTypes.Name, "a@b.com")}, "test"));

            var subscriptionController = new SubscriptionController(_subscriptionRepository.Object, _bookRepository.Object, _loggerMock.Object);
            subscriptionController.ControllerContext.HttpContext = new DefaultHttpContext { User = user };

            //Act
            var actionResult = await subscriptionController.PurchaseSubscription(new SubscribeData() { BookId = 1, PurchasePrice = 120 }) as OkObjectResult;

            //Assert
            Assert.Equal(actionResult.StatusCode, (int)System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task Subscribe_notfound_request()
        {
            //Arrange
            var fakeBook = Enumerable.Empty<Book>();
            _bookRepository.Setup(x => x.FindByIdsAsync(It.IsAny<int[]>())).Returns(Task.FromResult(fakeBook));

            var subscriptionController = new SubscriptionController(_subscriptionRepository.Object, _bookRepository.Object, _loggerMock.Object);

            //Act
            var actionResult = await subscriptionController.PurchaseSubscription(new SubscribeData() { BookId = 1, PurchasePrice = 120 }) as NotFoundObjectResult;
            //Assert
            Assert.Equal(actionResult.StatusCode, (int)System.Net.HttpStatusCode.NotFound);
        }


        private Book GetFakeBookData()
        {
            return new Book("Professional C#", "sample text", 120);
        }
    }
}
