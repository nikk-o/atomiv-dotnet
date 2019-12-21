﻿using Optivem.Framework.Core.Application;
using Optivem.Template.Core.Application.Products.Responses;

namespace Optivem.Template.Core.Application.Products.Requests
{
    public class BrowseProductsRequest : IRequest<BrowseProductsResponse>
    {
        public int Page { get; set; }

        public int Size { get; set; }
    }
}