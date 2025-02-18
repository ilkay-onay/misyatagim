using MediatR;
using ProductService.Domain.Entities;

namespace ProductService.Application.Commands;

public class UpdateProductCommand : IRequest<Product>
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public decimal Price { get; set; }
    public string? Size { get; set; } // Yatak boyutu (örn. "90x190", "160x200")
    public string? Material { get; set; } // Yatak malzemesi (örn. "Pamuk", "Visco")
    public string? Color { get; set; } // Yatak rengi
    public string? Firmness { get; set; } // Yatak sertliği (örn. "Yumuşak", "Orta", "Sert")
     public string[]? ImageBase64s { get; set; }
}