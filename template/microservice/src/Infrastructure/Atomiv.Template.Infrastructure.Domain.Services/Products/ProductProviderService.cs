﻿using Atomiv.Template.Core.Domain.Products;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Atomiv.Template.Infrastructure.Domain.Services.Products
{
    public class ProductProviderService : IProductProviderService
    {
        private readonly IProductFactory _productFactory;

        public ProductProviderService(IProductFactory productFactory)
        {
            _productFactory = productFactory;
        }

        public Task<IEnumerable<Product>> GetProductsAsync()
        {
            // TODO: VC: Sample reading from DB, then also reading from web service etc.
            // and then combine those product lists together

            var product1 = _productFactory.CreateProduct("ABC", "Product ABC", 45.56m);
            var product2 = _productFactory.CreateProduct("DEF", "Product DEF", 56.78m);

            var products = new List<Product>
            {
                product1,
                product2,
            };

            return Task.FromResult(products.AsEnumerable());
        }
    }
}
