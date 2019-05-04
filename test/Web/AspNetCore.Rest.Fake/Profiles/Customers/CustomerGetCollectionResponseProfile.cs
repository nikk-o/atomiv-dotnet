﻿using Optivem.Framework.Web.AspNetCore.Rest.Fake.Dtos.Customers;
using Optivem.Framework.Web.AspNetCore.Rest.Fake.Entities;
using Optivem.Infrastructure.Mapping.AutoMapper;

namespace Optivem.Framework.Web.AspNetCore.Rest.Fake.Profiles.Customers
{
    public class CustomerGetCollectionResponseProfile : AutoMapperResponseProfile<Customer, CustomerGetCollectionResponse>
    {
    }
}
