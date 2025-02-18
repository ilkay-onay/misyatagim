   using MediatR;
    using CommentService.Application.Commands;
    using CommentService.Application.Interfaces;
    using System.Threading.Tasks;
   namespace CommentService.Application.Handlers;
   public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, bool>
    {
       private readonly ICommentRepository _commentRepository;
       public DeleteCommentCommandHandler(ICommentRepository commentRepository)
        {
           _commentRepository = commentRepository;
        }
      public async Task<bool> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
      {
          var comment = await _commentRepository.GetByIdAsync(request.Id);
            if (comment == null)
                return false;

            await _commentRepository.DeleteAsync(comment);
           return true;
     }
     }