using CommentService.Domain.Entities;

namespace CommentService.Application.Interfaces;

public interface ICommentRepository
{
 Task<List<Comment>> GetByProductIdAsync(int productId);
 Task AddAsync(Comment comment);
 Task<Comment?> GetByIdAsync(string id);
 Task DeleteAsync(Comment comment);
   Task UpdateAsync(Comment comment);
    Task<List<Comment>> GetByProductIdWithUserAsync(int productId); //changed
}