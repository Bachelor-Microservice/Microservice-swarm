using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer.Entities;
using IdentityServer.Quickstart.Usermanager.DTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Quickstart.Usermanager
{
    [SecurityHeaders]
    [AllowAnonymous]
    [Route("usermanager")]
    public class UserManagerController : Controller
    {
        private readonly UserManager<ApplicationUser> _usermanager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserManagerController(UserManager<ApplicationUser> usermanager , RoleManager<IdentityRole> roleManager , SignInManager<ApplicationUser> signInManager )
        {
            _usermanager = usermanager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody]CreateUserCommand command)
        {
            var user = new ApplicationUser
            {
                Email = command.Email,
                UserName = command.Username
            };

            var result = await _usermanager.CreateAsync(user, command.Password);
            await _usermanager.AddToRoleAsync(user, "user");
            if (!result.Succeeded)
            {
                return BadRequest();
            }
            
            return Ok();
            
        }
        
        [HttpPut]
        public async Task<IActionResult> UpdatePassword([FromBody]UpdatePasswordCommand command)
        {
            var user = await _usermanager.FindByIdAsync(command.UserId.ToString());
            var result = await _usermanager.ChangePasswordAsync(user, command.CurrentPassword, command.NewPassword);
            if (!result.Succeeded)
            {
                return BadRequest();
            }
            
            return Ok();
        }
        
        [HttpPut]
        [Route("email")]
        public async Task<IActionResult> UpdateEmail([FromBody]UpdateEmailCommand command)
        {
            var user = await _usermanager.FindByIdAsync(command.UserId.ToString());
            var token = await _usermanager.GenerateChangeEmailTokenAsync(user, command.Email);
            var result = await _usermanager.ChangeEmailAsync(user, command.Email, token);
            if (!result.Succeeded)
            {
                return BadRequest();
            }
            return Ok();
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _usermanager.Users.ToListAsync();
            if (users == null)
            {
                return BadRequest();
            }
            return Ok(users);
        }
        
        [HttpDelete]
        public async Task<IActionResult> DeleteUser([FromQuery]Guid id)
        {
            await HttpContext.SignOutAsync();
            await _signInManager.SignOutAsync();
            var user = await _usermanager.FindByIdAsync(id.ToString());
            var result = await _usermanager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}