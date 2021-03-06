﻿using Atomiv.Core.Application;
using Atomiv.Template.Core.Application.Commands.Products;
using Atomiv.Template.Core.Domain.Products;
using System.Threading.Tasks;

namespace Atomiv.Template.Core.Application.Commands.Handlers.Products
{
    public class UnlistProductCommandHandler : IRequestHandler<UnlistProductCommand, UnlistProductCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public UnlistProductCommandHandler(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<UnlistProductCommandResponse> HandleAsync(UnlistProductCommand request)
        {
            var productId = new ProductIdentity(request.Id);

            var product = await _productRepository.FindAsync(productId);

            if(product == null)
            {
                throw new ExistenceException();
            }

            product.Unlist();

            await _productRepository.UpdateAsync(product);
            return _mapper.Map<Product, UnlistProductCommandResponse>(product);
        }
    }
}