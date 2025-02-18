using MediatR;
using CommentService.Domain.Entities;
using CommentService.Application.Commands;
using CommentService.Application.Interfaces;
using CommentService.Infrastructure.Services;

namespace CommentService.Application.Handlers;

public class AddCommentCommandHandler : IRequestHandler<AddCommentCommand, Comment>
{
    private readonly ICommentRepository _commentRepository;
    private readonly RabbitMQProducer _rabbitMQProducer;

    public AddCommentCommandHandler(ICommentRepository commentRepository, RabbitMQProducer rabbitMQProducer)
    {
        _commentRepository = commentRepository;
        _rabbitMQProducer = rabbitMQProducer;
    }

    public async Task<Comment> Handle(AddCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = new Comment
        {
            ProductId = request.ProductId,
            Text = request.Text,
            UserId = request.UserId,
            Username = request.Username // Add Username
        };

        await _commentRepository.AddAsync(comment);
        _rabbitMQProducer.Publish(comment);
        return comment;
    }
}