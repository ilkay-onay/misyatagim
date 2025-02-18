using System;

namespace CommentService.Domain.Entities;

public class Comment
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public int ProductId { get; set; }
    public string? UserId { get; set; }
    public string? Username { get; set; } // Add Username field
    public string Text { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}