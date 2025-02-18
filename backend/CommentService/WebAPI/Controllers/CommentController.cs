using MediatR;
using Microsoft.AspNetCore.Mvc;
using CommentService.Application.Queries;
using CommentService.Application.Commands;
using CommentService.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace CommentService.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentController : ControllerBase
{
    private readonly IMediator _mediator;

    public CommentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{productId}")]
    public async Task<ActionResult<List<Comment>>> GetComments(int productId)
    {
        var query = new GetCommentsQuery { ProductId = productId };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Comment>> AddComment(AddCommentCommand command)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // User Id'yi token'dan alıyoruz.
        command.UserId = userId;
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteComment(string id)
    {
        var result = await _mediator.Send(new DeleteCommentCommand { Id = id });
        return Ok(result);
    }
}