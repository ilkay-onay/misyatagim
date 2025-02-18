namespace ProductService.Domain.Entities;

public class Product
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public decimal Price { get; set; }
    public string Slug { get; set; } = string.Empty; 
    public string[] ImageBase64s { get; set; } = new string[0]; // Base64 formatında resimler
    public string? Size { get; set; } // Yatak boyutu (örn. "90x190", "160x200")
    public string? Material { get; set; } // Yatak malzemesi (örn. "Pamuk", "Visco")
    public string? Color { get; set; } // Yatak rengi
    public string? Firmness { get; set; } // Yatak sertliği (örn. "Yumuşak", "Orta", "Sert")
     public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; } //nullable olmalı ilk başta boş olabilir.
}