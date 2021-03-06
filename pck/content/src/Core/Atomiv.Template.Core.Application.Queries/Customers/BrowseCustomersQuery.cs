﻿using Atomiv.Core.Application;

namespace Atomiv.Template.Core.Application.Queries.Customers
{
    public class BrowseCustomersQuery : IRequest<BrowseCustomersQueryResponse>
    {
        public int Page { get; set; }

        public int Size { get; set; }
    }
}