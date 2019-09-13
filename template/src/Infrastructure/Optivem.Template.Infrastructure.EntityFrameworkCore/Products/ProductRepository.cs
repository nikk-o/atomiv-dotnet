﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Optivem.Framework.Core.Common.Mapping;
using Optivem.Framework.Infrastructure.EntityFrameworkCore;
using Optivem.Template.Core.Domain.Products;

namespace Optivem.Template.Infrastructure.EntityFrameworkCore.Products
{
    public class ProductRepository : CrudRepository<DatabaseContext, Product, ProductIdentity, ProductRecord, int>, IProductRepository
    {
        public ProductRepository(IMapper mapper, DatabaseContext context) : base(mapper, context)
        {
        }

        public IEnumerable<Product> Get(int page, int size)
        {
            var skip = page * size;
            var records = ReadonlySet.Skip(skip).Take(size).Select(GetAggregateRoot).ToList();
            return records;
        }

        public Task<IEnumerable<Product>> GetAsync(int page, int size)
        {
            var records = Get(page, size);
            return Task.FromResult(records);
        }

        protected override Product GetAggregateRoot(ProductRecord record)
        {
            var identity = GetIdentity(record);
            return new Product(identity, record.ProductCode, record.ProductName, record.ListPrice);
        }

        protected override ProductIdentity GetIdentity(ProductRecord record)
        {
            var id = record.Id;
            return new ProductIdentity(id);
        }

        protected override ProductRecord GetRecord(ProductIdentity identity)
        {
            return new ProductRecord
            {
                Id = identity.Id,
            };
        }

        protected override ProductRecord GetRecord(Product aggregateRoot)
        {
            return new ProductRecord
            {
                Id = aggregateRoot.Id.Id,
                ProductCode = aggregateRoot.ProductCode,
                ProductName = aggregateRoot.ProductName,
                ListPrice = aggregateRoot.ListPrice,
            };
        }
    }
}
