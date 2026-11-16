using Application.Comments;
using Application.Comments.Create;
using Application.Comments.Delete;
using Application.Comments.GetAll;
using Application.Comments.GetById;
using Application.Comments.Update;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Contracts.Comments;
using WebAPI.Extensions;

namespace WebAPI.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(List<CommentDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var comments = await mediator.Send(new GetAllCommentsQuery(), ct);
            return Ok(comments);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(CommentDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken ct)
        {
            var comment = await mediator.Send(new GetCommentByIdQuery(id), ct);
            return Ok(comment);
        }

        [HttpPost("{stokeId:int}")]
        [Authorize]
        [ProducesResponseType(typeof(CommentDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create([FromRoute] int stokeId, [FromBody] CreateCommentRequest request, CancellationToken ct)
        {
            var command = new CreateCommentCommand
            {
                StokeId = stokeId,
                UserId = User.GetUserId(),
                Title = request.Title,
                Content = request.Content
            };

            var comment = await mediator.Send(command, ct);
            return CreatedAtAction(nameof(GetById), new { id = comment.Id }, comment);
        }

        [HttpPut("{id:int}")]
        [Authorize]
        [ProducesResponseType(typeof(CommentDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequest request, CancellationToken ct)
        {
            var command = new UpdateCommentCommand
            {
                CommentId = id,
                UserId = User.GetUserId(),
                Title = request.Title,
                Content = request.Content
            };

            var comment = await mediator.Send(command, ct);
            return Ok(comment);
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken ct)
        {
            await mediator.Send(new DeleteCommentCommand(id, User.GetUserId()), ct);
            return NoContent();
        }
    }
}
