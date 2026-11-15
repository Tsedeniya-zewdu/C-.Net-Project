using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Extension;
using api.interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController:ControllerBase
    {
        private readonly IcommentRepository _commentRepo;
         private readonly IStokeRepository _stokeRepo;
         private readonly UserManager<AppUser> _userManager;
      public CommentController(IcommentRepository commentRepo, IStokeRepository stokeRepo, UserManager<AppUser> userManager)
      {
        _commentRepo=commentRepo;
        _stokeRepo=stokeRepo;
        _userManager=userManager;
      } 
      [HttpGet]

      public async Task<IActionResult> GetAll()

        {
      
      
            var comments = await _commentRepo.GetAllAsync();
            var commentDto = comments.Select(s=>s.ToCommentDto());
            return Ok(commentDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
    {
      var comment = await _commentRepo.GetByIdAsync(id);
      if(comment == null)
      {
        return NotFound();
      }
      return Ok(comment.ToCommentDto());
    }
[HttpPost("{StokeId:int}")]
public async Task<IActionResult> Create([FromRoute] int StokeId,[FromBody] CreateCommentDto commentDto)
    {
      if (!ModelState.IsValid)
      
        return BadRequest(ModelState);
      if(!await _stokeRepo.StokeExists(StokeId))
      {
        return BadRequest("stoke does not exist");
      }

      var username = User.GetUserName();
      var appUser = await _userManager.FindByNameAsync(username);
  

      var commentModel =commentDto.ToCommentFromCreate(StokeId);
      commentModel.AppUserId=appUser.Id;
      await _commentRepo.CreateAsync(commentModel);
       return CreatedAtAction(
                nameof(GetById),
                new { id = commentModel.Id },
                commentModel.ToCommentDto()
            );
    }
    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> UpdateComment([FromRoute] int id, UpdateCommentRequestDto updatedto)
    {
      if (!ModelState.IsValid)
      
        return BadRequest(ModelState);
      var comment = await _commentRepo.UpdateAsync(id,updatedto.ToCommentFromUpate() );
      if (comment == null)
      {
        return NotFound("comment not found");
      }
      return Ok(comment.ToCommentDto());
    }
    [HttpDelete]
    [Route("{id:int}")]
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