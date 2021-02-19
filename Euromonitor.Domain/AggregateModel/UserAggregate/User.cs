using System;
using Euromonitor.Domain.SeedWork;

namespace Euromonitor.Domain.AggregateModel.UserAggregate
{
    public class User : Entity, IAggregateRoot
    {

        public string Email { get; set; }

        protected User() { }
        public User(string email) 
        {
            Email = email;
        }

    }
}
