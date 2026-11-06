using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController:ControllerBase
    {
        private readonly IcommentRepository _commentRepo;
         private readonly IStokeRepository _stokeRepo;
      public CommentController(IcommentRepository commentRepo, IStokeRepository stokeRepo)
      {
        _commentRepo=commentRepo;
        _stokeRepo=stokeRepo;
      } 
      [HttpGet]

      public async Task<IActionResult> GetAll()
        {
            var comments = await _commentRepo.GetAllAsync();
            var commentDto = comments.Select(s=>s.ToCommentDto());
            return Ok(commentDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
    {
      var comment = await _commentRepo.GetByIdAsync(id);
      if(comment == null)
      {
        return NotFound();
      }
      return Ok(comment.ToCommentDto());
    }
[HttpPost("{StokeId}")]
public async Task<IActionResult> Create([FromRoute] int StokeId,[FromBody] CreateCommentDto commentDto)
    {
      if(!await _stokeRepo.StokeExists(StokeId))
      {
        return BadRequest("stoke does not exist");
      }
      var commentModel =commentDto.ToCommentFromCreate(StokeId);
      await _commentRepo.CreateAsync(commentModel);
       return CreatedAtAction(
                nameof(GetById),
                new { id = commentModel.Id },
                commentModel.ToCommentDto()
            );
    }
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateComment([FromRoute] int id, UpdateCommentRequestDto updatedto)
    {
      var comment = await _commentRepo.UpdateAsync(id,updatedto.ToCommentFromUpate() );
      if (comment == null)
      {
        return NotFound("comment not found");
      }
      return Ok(comment.ToCommentDto());
    }
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteComment([FromRoute] int id)
    {
      var commentModel = await _commentRepo.DeleteCommentAsync(id);
      if(commentModel == null)
      {
        return NotFound("comment does not exist");
      }
return Ok(commentModel);
    }
    }
}