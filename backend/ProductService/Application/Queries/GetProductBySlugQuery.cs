using MediatR;
using ProductService.Domain.Entities;

namespace ProductService.Application.Queries;

public class GetProductBySlugQuery : IRequest<Product?>
{
    public string Slug { get; set; } = string.Empty;
}