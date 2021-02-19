using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Euromonitor.ApI.Models;
using Euromonitor.Domain.AggregateModel.BookAggregate;
using Euromonitor.Domain.AggregateModel.SubscriptionAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Euromonitor.ApI.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IBookRepository _bookRepository;
        private readonly ILogger<SubscriptionController> _logger;
        public SubscriptionController(ISubscriptionRepository subscriptionRepository, IBookRepository bookRepository, ILogger<SubscriptionController> logger)
        {
            _subscriptionRepository = subscriptionRepository;
            _bookRepository = bookRepository;
            _logger = logger;
        }


        [HttpPost]
        [Route("subscribe")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(SubscribeData), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> PurchaseSubscription([FromBody] SubscribeData model)
        {
            try
            {
                var books = await _bookRepository.FindByIdsAsync(new int[] { model.BookId });
                if (books.Count() < 1) return NotFound("requested book not found");

                var sub = new Subscription(User.Identity.Name, model.BookId, model.PurchasePrice);
                await _subscriptionRepository.Save(sub);

                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError($"FAILED: - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
           
        }


        [HttpPut]
        [Route("unsubscribe")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> UnSubscribe([FromBody] int bookId)
        {
            try
            {
                var subscription = await _subscriptionRepository.FinByUserBookAsync(User.Identity.Name, bookId);
                if (subscription == null) return NotFound();

                subscription.UnSubscribe();
                await _subscriptionRepository.Update(subscription);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"FAILED: - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
           
        }
    }

}
