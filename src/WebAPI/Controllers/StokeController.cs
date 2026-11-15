
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Stoke;
using Domain.Interfaces;
using Application.Mappers;
using Microsoft.AspNetCore.Mvc;
using Domain.Filters;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    [Route("api/stoke")]
    [ApiController]
    public class StokeController : ControllerBase
    {
        private readonly IStokeRepository _StokeRepo;

        public StokeController(IStokeRepository StokeRepo)
        {
            _StokeRepo = StokeRepo;
        }

        // GET ALL
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] QueryObjects query)
        {
            var stokes = await _StokeRepo.GetAllAsync(query);

            var stokesDto = stokes.Select(s => s.ToStokeDto()).ToList();

            return Ok(stokesDto);
        }

        // GET BY ID
        [HttpGet("{stokeId:int}")]
        public async Task<IActionResult> GetById([FromRoute] int stokeId)
        {
            var stoke = await _StokeRepo.GetByIdAsync(stokeId);

            if (stoke == null)
            {
                return NotFound();
            }

            return Ok(stoke.ToStokeDto());
        }

        // CREATE
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateStokerequestDto StokeDto)
        {
            if (!ModelState.IsValid)
      
        return BadRequest(ModelState);
            var StokeModel = StokeDto.ToStokeFromCreateDto();

            await _StokeRepo.CreateAsync(StokeModel);

            return CreatedAtAction(
                nameof(GetById),
                new { stokeId = StokeModel.StokeId },
                StokeModel.ToStokeDto()
            );
        }

        // UPDATE
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
            [FromRoute] int id,
            [FromBody] UpdateStokeRequestDto updateDto)
        {
            if (!ModelState.IsValid)
      
        return BadRequest(ModelState);
            var stokeModel = await _StokeRepo.GetByIdAsync(id);

            if (stokeModel == null)
            {
                return NotFound();
            }

            stokeModel.UpdateFromDto(updateDto);
            await _StokeRepo.UpdateAsync(stokeModel);

            return Ok(stokeModel.ToStokeDto());
        }

        // DELETE
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stokeModel = await _StokeRepo.DeleteAsync(id);

            if (stokeModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
