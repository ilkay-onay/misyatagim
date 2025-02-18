using MediatR;

namespace CommentService.Application.Commands;

public class DeleteCommentCommand : IRequest<bool>
{
    public required string Id { get; set; } // required eklendi
}