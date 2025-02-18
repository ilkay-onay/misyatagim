using MediatR;

namespace ProductService.Application.Commands;

public class DeleteProductCommand : IRequest<bool>
{
    public int Id { get; set; }
}