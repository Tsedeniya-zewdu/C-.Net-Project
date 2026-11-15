using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Extensions;
using Domain.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{

    [Route("api/Portfoio")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStokeRepository _stokeRepo;
        private readonly IPortfolioRepository _portfolioRepo;
        public PortfolioController(UserManager<AppUser> userManager, IStokeRepository stokeRepo, IPortfolioRepository portfolioRepo)
        {
            _userManager = userManager;
            _stokeRepo = stokeRepo;
            _portfolioRepo = portfolioRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfoio()
        {
            var username = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(username);
            var userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);
            return Ok(userPortfolio);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio(string symbol)
        {
            var username = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(username);
            var Stoke = await _stokeRepo.GetBySymbolAsync(symbol);

            if (Stoke == null) return BadRequest("Stock not found");
            var userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);
            if (userPortfolio.Any(e => e.Symbol.ToLower() == symbol.ToLower())) return BadRequest("Cannot add same stoke to portfolio");

            var PortfolioModel = new Portfolio
            {
                StokeId = Stoke.StokeId,
                AppUserId = appUser.Id
            };
            await _portfolioRepo.CreateAsyn(PortfolioModel);
            if(PortfolioModel == null)
            {
                return StatusCode(500,"could not create");

            }
            else
            {
                return Created();
            }

        }
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePortfolio(string symbol)
        {
             var username = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(username);
            var userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);
            var filteredStoke = userPortfolio.Where(s=> s.Symbol.ToLower() == symbol.ToLower()).ToList();
            if(filteredStoke.Count() == 1)
            {
                await _portfolioRepo.DeletePortfolio(appUser,symbol);
            }
            else
            {
                return BadRequest("stock not found in you portfolio");
            }
            return Ok();
        }
    }
}