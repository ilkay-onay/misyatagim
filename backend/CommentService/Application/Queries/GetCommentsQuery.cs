using MediatR;
using CommentService.Domain.Entities;

namespace CommentService.Application.Queries;

public class GetCommentsQuery : IRequest<List<Comment>>
{
    public int ProductId { get; set; }
}