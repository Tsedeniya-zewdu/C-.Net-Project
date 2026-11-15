
using System;
using api.Dtos.Account;
using api.interfaces;
using api.Models;
using api.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenservice;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, ITokenService tokenservice, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenservice = tokenservice;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // 1. Create a new user object
                var appUser = new AppUser
                {
                    UserName = registerDto.UserName!,
                    Email = registerDto.Email
                };

                // 2. Create the user and hash the password
                var createdUser = await _userManager.CreateAsync(
                    appUser,
                    registerDto.Password!
                );

                // 3. Check whether user creation succeeded
                if (!createdUser.Succeeded)
                {
                    return BadRequest(createdUser.Errors);
                }

                // 4. Assign the default User role
                var roleResult = await _userManager.AddToRoleAsync(
                    appUser,
                    "User"
                );

                if (!roleResult.Succeeded)
                {
                    await _userManager.DeleteAsync(appUser);
                    return BadRequest(roleResult.Errors);
                }

                // 5. Return success
                return Ok(new NewUserDto
                {
            
                    UserName = appUser.UserName,
                    Email = appUser.Email,
                    Token = _tokenservice.CreateToken(appUser)
                });
            }
            catch (Exception ex)
{
    Console.WriteLine(ex.ToString());

    return StatusCode(500, ex.Message);
}
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                            return BadRequest(ModelState);
            
        var user = await _userManager.FindByNameAsync(loginDto.UserName);
            if(user == null) return Unauthorized("Invalid Username");
            var result = await _signInManager.CheckPasswordSignInAsync(user,loginDto.Password,false);
            if (!result.Succeeded)
            {
                return Unauthorized("username not found or password incorrect");
            }
            return Ok(
                new NewUserDto
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = _tokenservice.CreateToken(user)
                }
            );
        }
    }
}
