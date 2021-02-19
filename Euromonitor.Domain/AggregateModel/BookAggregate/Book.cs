using System;
using Euromonitor.Domain.SeedWork;

namespace Euromonitor.Domain.AggregateModel.BookAggregate
{

    public class Book : Entity, IAggregateRoot
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public decimal Price { get; set; }

        protected Book(){}
        public Book(string name, string text, decimal purchasePrice)
        {

            Name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentNullException(nameof(name));
            Text = !string.IsNullOrWhiteSpace(text) ? text : throw new ArgumentNullException(nameof(text));      
            Price = purchasePrice > 0 ? purchasePrice : throw new ArgumentNullException(nameof(purchasePrice));
        }

    }
}
