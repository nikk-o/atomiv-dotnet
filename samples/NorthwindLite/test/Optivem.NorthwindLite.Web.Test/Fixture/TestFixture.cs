﻿using Optivem.NorthwindLite.Infrastructure.Persistence;
using Optivem.Test.AspNetCore.EntityFrameworkCore.Xunit;

namespace Optivem.NorthwindLite.Web.Test.Fixture
{
    public class TestFixture : BaseTestClientFixture<TestClient, Startup, DatabaseContext>
    {
        public TestFixture(TestClient fixture)
            : base(fixture)
        {
        }

        protected override void Cleanup()
        {
            // TODO: VC: Optimize, use truncate, also generic truncate for all DB contexts, but only the operational tables

            using (var context = Fixture.Client.CreateDatabaseContext())
            {
                context.Customer.RemoveRange(context.Customer);
                context.SaveChanges();
            }
        }
    }
}