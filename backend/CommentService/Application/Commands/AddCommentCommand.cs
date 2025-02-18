using MediatR;
using CommentService.Domain.Entities;

namespace CommentService.Application.Commands;

public class AddCommentCommand : IRequest<Comment>
{
    public int ProductId { get; set; }
    public string Text { get; set; } = string.Empty;
    public string? UserId { get; set; }
    public string? Username { get; set; } // Add Username
}