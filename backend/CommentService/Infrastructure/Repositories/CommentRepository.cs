using MongoDB.Driver;
using CommentService.Domain.Entities;
using CommentService.Application.Interfaces;
using CommentService.Infrastructure.Data;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CommentService.Infrastructure.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly MongoDbContext _context;
   

    public CommentRepository(MongoDbContext context)
    {
        _context = context;
    }

    public async Task<List<Comment>> GetByProductIdAsync(int productId)
    {
         return await _context.Comments.Find(c => c.ProductId == productId).ToListAsync();
    }
       public async Task<List<Comment>> GetByProductIdWithUserAsync(int productId)
       {
           var comments = await _context.Comments.Find(c => c.ProductId == productId).ToListAsync();
           return comments;
        }

  public async Task AddAsync(Comment comment)
  {
      await _context.Comments.InsertOneAsync(comment);
  }

    public async Task<Comment?> GetByIdAsync(string id)
    {
        return await _context.Comments.Find(c => c.Id == id).FirstOrDefaultAsync();
    }

    public async Task DeleteAsync(Comment comment)
    {
        await _context.Comments.DeleteOneAsync(c => c.Id == comment.Id);
    }

    public async Task UpdateAsync(Comment comment)
    {
        await _context.Comments.ReplaceOneAsync(c => c.Id == comment.Id, comment);
    }
}