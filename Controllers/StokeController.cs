
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stoke;
using api.interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
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
        public async Task<IActionResult> GetAll()
        {
            var stokes = await _StokeRepo.GetAllAsync();

            var stokesDto = stokes.Select(s => s.ToStokeDto());

            return Ok(stokesDto);
        }

        // GET BY ID
        [HttpGet("{stokeId}")]
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
            var StokeModel = StokeDto.ToStokeFromCreateDto();

            await _StokeRepo.CreateAsync(StokeModel);

            return CreatedAtAction(
                nameof(GetById),
                new { stokeId = StokeModel.StokeId },
                StokeModel.ToStokeDto()
            );
        }

        // UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            [FromRoute] int id,
            [FromBody] UpdateStokeRequestDto updateDto)
        {
            var stokeModel = await _StokeRepo.UpdateAsync(id, updateDto);

            if (stokeModel == null)
            {
                return NotFound();
            }

            return Ok(stokeModel.ToStokeDto());
        }

        // DELETE
        [HttpDelete("{id}")]
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
