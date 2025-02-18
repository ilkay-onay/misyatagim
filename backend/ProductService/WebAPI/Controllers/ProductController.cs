// ProductController.cs
using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Interfaces;
using ProductService.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductService.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _productRepository.GetAllAsync();
            if (products == null || products.Count == 0)
            {
                return NotFound("No products found."); // Ürün yoksa 404 döndür
            }
            return Ok(products); // Ürünler varsa 200 ile döndür
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound(); // Ürün bulunamazsa 404 döndür
            }
            return Ok(product); // Ürün varsa 200 ile döndür
        }

        [HttpGet("slug/{slug}")]
        public async Task<ActionResult<Product>> GetProductBySlug(string slug)
        {
            var product = await _productRepository.GetBySlugAsync(slug);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            await _productRepository.AddAsync(product);
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> UpdateProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }
            await _productRepository.UpdateAsync(product);
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteProduct(int id)
        {
            var result = await _productRepository.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}