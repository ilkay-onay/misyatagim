// IProductRepository.cs
using ProductService.Domain.Entities;
using System.Threading.Tasks;

namespace ProductService.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();
        Task AddAsync(Product product);
        Task<Product?> GetByIdAsync(int id);
        Task UpdateAsync(Product product);
        Task<bool> DeleteAsync(int id); // int parametresi bekleniyor
        Task<Product?> GetBySlugAsync(string slug);
    }
}