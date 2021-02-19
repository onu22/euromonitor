using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
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
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly ILogger<BooksController> _logger;
        public BooksController(IBookRepository bookRepository, ISubscriptionRepository subscriptionRepository, ILogger<BooksController> logger)
        {
            _subscriptionRepository = subscriptionRepository;
            _bookRepository = bookRepository;
            _logger = logger;
        }


        [AllowAnonymous]
        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(IEnumerable<BookData>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBooks()
        {

            var books = await _bookRepository.FindAllAsync();
            var subs = await _subscriptionRepository.FindByUserActivesAsync(User.Identity.Name);
            var bookIds = subs.Select(s => s.BookId);

            List<BookData> bookdata = new List<BookData>();
            foreach (var book in books)
              bookdata.Add(MapBookModel(book, bookIds.Contains(book.Id)));
           
            return Ok(bookdata); 
        }

        [HttpGet]
        [Route("byid/{id:int}")]
        [ProducesResponseType(typeof(BookData), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetBookDetail(int id)
        {
         
            var book = await _bookRepository.FindByIdAsync(id);
            if (book == null) return NotFound("book not found");

            var subs = await _subscriptionRepository.FindByUserActivesAsync(User.Identity.Name);         
            var bookIds = subs.Select(s => s.BookId);
            return Ok(MapBookModel(book, bookIds.Contains(book.Id)));  
        }


        [HttpGet]
        [Route("subscribedbooks")]
        [ProducesResponseType(typeof(IEnumerable<Book>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetUserBooks()
        {
            try
            {
                var subs = await _subscriptionRepository.FindByUserActivesAsync(User.Identity.Name);
                if (subs.Count() < 1) return NotFound("no active subscription found");

                var books = await _bookRepository.FindByIdsAsync(subs.Select(s => s.BookId));
                return Ok(books);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"FAILED: - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        private BookData MapBookModel(Book book, bool userIsSubscribed)
        {
            return new BookData()
            {
                Id = book.Id,
                Name = book.Name,
                Text = book.Text,
                Price = book.Price,
                UserIsSubscribed = userIsSubscribed,
            };
        }


    }
}
