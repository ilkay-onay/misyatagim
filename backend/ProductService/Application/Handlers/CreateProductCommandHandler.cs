using MediatR;
using ProductService.Domain.Entities;
using ProductService.Application.Commands;
using ProductService.Application.Interfaces;
using System.Threading.Tasks;
using ProductService.Infrastructure.Services;

namespace ProductService.Application.Handlers;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Product>
{
    private readonly IProductRepository _productRepository;
      private readonly RedisService _redisService;

    public CreateProductCommandHandler(IProductRepository productRepository, RedisService redisService)
    {
        _productRepository = productRepository;
          _redisService = redisService;
    }

    public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
       var product = new Product
       {
         Name = request.Name,
         Description = request.Description,
         Price = request.Price,
         Slug =  Slugify.GenerateSlug(request.Name),
         Size = request.Size,
         Material = request.Material,
         Color = request.Color,
         Firmness = request.Firmness,
         ImageBase64s = request.ImageBase64s != null && request.ImageBase64s.Any() ? request.ImageBase64s : new string[0],
    };
    
        await _productRepository.AddAsync(product);
         await _redisService.RemoveAsync("products");
        return product;
    }
}