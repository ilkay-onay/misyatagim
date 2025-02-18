// DeleteProductCommandHandler.cs
using MediatR;
using ProductService.Application.Commands;
using ProductService.Application.Interfaces;
using System.Threading.Tasks;

namespace ProductService.Application.Handlers
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);
            if (product == null)
            {
                return false; // Ürün bulunamadı
            }

            // DeleteAsync metoduna ürün ID'sini geçir
            return await _productRepository.DeleteAsync(product.Id);
        }
    }
}