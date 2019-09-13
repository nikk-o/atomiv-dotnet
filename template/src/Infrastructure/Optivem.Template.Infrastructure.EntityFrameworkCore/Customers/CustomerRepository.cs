﻿using Optivem.Framework.Core.Common.Mapping;
using Optivem.Framework.Infrastructure.EntityFrameworkCore;
using Optivem.Template.Core.Domain.Customers;

namespace Optivem.Template.Infrastructure.EntityFrameworkCore.Customers
{
    public class CustomerRepository : CrudRepository<DatabaseContext, Customer, CustomerIdentity, CustomerRecord, int>, ICustomerRepository
    {
        public CustomerRepository(IMapper mapper, DatabaseContext context) 
            : base(mapper, context)
        {
        }

        protected override Customer GetAggregateRoot(CustomerRecord record)
        {
            var identity = new CustomerIdentity(record.Id);
            return new Customer(identity, record.FirstName, record.LastName);
        }

        protected override CustomerIdentity GetIdentity(CustomerRecord record)
        {
            return new CustomerIdentity(record.Id);
        }

        protected override CustomerRecord GetRecord(CustomerIdentity identity)
        {
            return new CustomerRecord
            {
                Id = identity.Id,
            };
        }

        protected override CustomerRecord GetRecord(Customer aggregateRoot)
        {
            return new CustomerRecord
            {
                Id = aggregateRoot.Id.Id,
                FirstName = aggregateRoot.FirstName,
                LastName = aggregateRoot.LastName,
                Order = null,
            };
        }
    }
}