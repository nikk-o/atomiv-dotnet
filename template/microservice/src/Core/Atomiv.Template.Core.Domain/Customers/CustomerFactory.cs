﻿using Atomiv.Core.Domain;

namespace Atomiv.Template.Core.Domain.Customers
{
    public class CustomerFactory : ICustomerFactory
    {
        private readonly IIdentityGenerator<CustomerIdentity> _customerIdentityGenerator;

        public CustomerFactory(IIdentityGenerator<CustomerIdentity> customerIdentityGenerator)
        {
            _customerIdentityGenerator = customerIdentityGenerator;
        }

        public Customer CreateCustomer(string firstName, string lastName)
        {
            var id = _customerIdentityGenerator.Next();

            return new Customer(id, firstName, lastName);
        }
    }
}
