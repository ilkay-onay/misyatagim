 using MediatR;
 using CommentService.Domain.Entities;
 using CommentService.Application.Queries;
 using CommentService.Application.Interfaces;

 namespace CommentService.Application.Handlers;

 public class GetCommentsQueryHandler : IRequestHandler<GetCommentsQuery, List<Comment>>
 {
     private readonly ICommentRepository _commentRepository;

     public GetCommentsQueryHandler(ICommentRepository commentRepository)
     {
         _commentRepository = commentRepository;
     }

     public async Task<List<Comment>> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
     {
         return await _commentRepository.GetByProductIdWithUserAsync(request.ProductId);
     }
 }