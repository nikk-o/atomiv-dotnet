﻿using Atomiv.Core.Application;
using Atomiv.Template.Core.Application.Commands.Products;
using Atomiv.Template.Core.Domain.Products;
using System.Threading.Tasks;

namespace Atomiv.Template.Core.Application.Commands.Handlers.Products
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly IProductFactory _productFactory;

        public CreateProductCommandHandler(IMapper mapper, IProductRepository productRepository, IProductFactory productFactory)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _productFactory = productFactory;
        }

        public async Task<CreateProductCommandResponse> HandleAsync(CreateProductCommand request)
        {
            var product = GetProduct(request);

            await _productRepository.AddAsync(product);

            return _mapper.Map<Product, CreateProductCommandResponse>(product);
        }

        private Product GetProduct(CreateProductCommand request)
        {
            var productCode = request.Code;
            var productName = request.Description;
            var listPrice = request.UnitPrice;

            return _productFactory.CreateProduct(productCode, productName, listPrice);
        }
    }
}