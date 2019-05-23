﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Optivem.Common.Http;
using Optivem.Infrastructure.Http.System;
using Optivem.Infrastructure.Persistence.EntityFrameworkCore;
using Optivem.NorthwindLite.Core.Application.Interface.Customers.Commands;
using Optivem.NorthwindLite.Core.Application.Interface.Customers.Queries.List;
using Optivem.NorthwindLite.Core.Application.Interface.Customers.Retrieve;
using Optivem.NorthwindLite.Core.Application.Interface.Requests.Customers;
using Optivem.NorthwindLite.Infrastructure.Persistence;
using Optivem.Test.Xunit.AspNetCore;
using System.Threading.Tasks;

namespace Optivem.NorthwindLite.Web.Test.Fixture
{
    // TODO: VC

    public class TestClient : BaseTestJsonClient<Startup>
    {
        public TestClient()
        {
            Customers = new CustomersControllerClient(ControllerClientFactory);
            ConnectionString = Configuration.GetConnectionString(Startup.DatabaseContextConnectionStringKey);
        }

        public CustomersControllerClient Customers { get; }

        // TODO: Move to AspNetCore.EntityFrameworkCore

        protected string ConnectionString { get; }

        protected DatabaseContext CreateContext()
        {
            var builder = new DbContextOptionsBuilder<DatabaseContext>()
                .UseSqlServer(ConnectionString);

            return new DatabaseContext(builder.Options);
        }

        public void EnsureDatabaseCreated()
        {
            using (var context = CreateContext())
            {
                context.Database.EnsureCreated();
            }
        }
    }

    public class CustomersControllerClient : BaseControllerClient
    {
        public CustomersControllerClient(IControllerClientFactory clientFactory)
            : base(clientFactory, "api/customers")
        {
        }

        public Task<IObjectClientResponse<ListCustomersResponse>> ListCustomersAsync()
        {
            return Client.GetAsync<ListCustomersResponse>();
        }

        public Task<IObjectClientResponse<CreateCustomerResponse>> CreateCustomerAsync(CreateCustomerRequest request)
        {
            return Client.PostAsync<CreateCustomerRequest, CreateCustomerResponse>(request);
        }

        public Task<IObjectClientResponse<FindCustomerResponse>> FindCustomerAsync(int id)
        {
            return Client.GetByIdAsync<int, FindCustomerResponse>(id);
        }
    }
}