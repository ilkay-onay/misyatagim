using MediatR;
using ProductService.Domain.Entities;

namespace ProductService.Application.Queries;

public class GetProductByIdQuery : IRequest<Product?>
{
    public int Id { get; set; }
}