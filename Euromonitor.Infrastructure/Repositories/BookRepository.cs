using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Euromonitor.Domain.AggregateModel.BookAggregate;
using System.Linq;

namespace Euromonitor.Infrastructure.Repositories
{
    public class BookRepository: IBookRepository
    {
        private readonly EuromonitorContext _context;

        public BookRepository(EuromonitorContext context) => _context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task<IEnumerable<Book>> FindAllAsync()
        {
            return  await _context.Books.ToListAsync();
        }

        public async Task<Book> FindByIdAsync(int id)
        {
            var query = _context.Books.Where(s => s.Id == id);
            return await query.SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Book>> FindByIdsAsync(IEnumerable<int> ids)
        {
             var query = _context.Books.Where(s => ids.Contains(s.Id));
             return await query.ToListAsync();
        }

    }
}
