using MediatR;
using ProductService.Domain.Entities;
using ProductService.Application.Queries;
using ProductService.Application.Interfaces;
using System.Threading.Tasks;

namespace ProductService.Application.Handlers;

public class GetProductBySlugQueryHandler : IRequestHandler<GetProductBySlugQuery, Product?>
{
    private readonly IProductRepository _productRepository;

    public GetProductBySlugQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Product?> Handle(GetProductBySlugQuery request, CancellationToken cancellationToken)
    {
        return await _productRepository.GetBySlugAsync(request.Slug);
    }
}