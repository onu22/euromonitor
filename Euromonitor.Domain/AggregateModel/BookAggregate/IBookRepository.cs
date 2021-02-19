using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Euromonitor.Domain.SeedWork;

namespace Euromonitor.Domain.AggregateModel.BookAggregate
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<IEnumerable<Book>> FindAllAsync();
        Task<IEnumerable<Book>> FindByIdsAsync(IEnumerable<int> ids);
        Task<Book> FindByIdAsync(int id);
    }
}
