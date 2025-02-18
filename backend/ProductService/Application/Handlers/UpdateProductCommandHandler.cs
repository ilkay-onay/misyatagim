using MediatR;
using ProductService.Domain.Entities;
using ProductService.Application.Commands;
using ProductService.Application.Interfaces;
using System.Threading.Tasks;
using ProductService.Infrastructure.Services;

namespace ProductService.Application.Handlers;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Product>
{
   private readonly IProductRepository _productRepository;
     private readonly RedisService _redisService;
   public UpdateProductCommandHandler(IProductRepository productRepository, RedisService redisService)
   {
       _productRepository = productRepository;
         _redisService = redisService;
   }

   public async Task<Product> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
   {
       var product = await _productRepository.GetByIdAsync(request.Id);

       if (product == null)
           throw new Exception("Product not found");

      product.Name = request.Name;
        product.Description = request.Description;
        product.Price = request.Price;
      product.Size = request.Size;
      product.Material = request.Material;
        product.Color = request.Color;
      product.Firmness = request.Firmness;
       product.ImageBase64s = request.ImageBase64s != null && request.ImageBase64s.Any() ? request.ImageBase64s : new string[0];

       await _productRepository.UpdateAsync(product);
        await _redisService.RemoveAsync("products");
       return product;
   }
}