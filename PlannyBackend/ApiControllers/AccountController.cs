using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlannyBackend.Dtos.Account;
using Microsoft.AspNetCore.Identity;
using PlannyBackend.Models.Identity;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using PlannyBackend.Bll.Interfaces;
using PlannyBackend.Interfaces;

namespace PlannyBackend.ApiControllers
{

    [Produces("application/json")]
    [Route("api/Account")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;

        public AccountController(
            ITokenService tokenService,
           UserManager<ApplicationUser> userManager,
           IUserService userService
          )
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _userService = userService;
        }

        [HttpPost("login")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(TokenDto), "JWT access token returned")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, null, "Wrong username or password")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            //todo elszáll ha nincs ilyen user. 
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return BadRequest("Invalid username or password.");
            }

            var token = await _tokenService.GetTokenForUserAsync(user);
            return Ok(token);
        }

        [HttpPost("register")]
        [SwaggerResponse((int)HttpStatusCode.OK, null, "Registration sucessful.")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, null, "Registration failed.")]

        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            if (model == null
                || string.IsNullOrEmpty(model.Email)
                || string.IsNullOrEmpty(model.UserName))
            {
                return BadRequest("Please fill al fields.");
            }

            var user = model.toApplicationUser();
            var (success, error) = await _userService.RegisterUser(user, model.Password);

            if (success)
            {
                return Ok("Registration sucessful.");
            }
            else
            {
                return BadRequest(error);
            }
        }
    }
}