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

namespace PlannyBackend.ApiControllers
{

    [Produces("application/json")]
    [Route("api/Account")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;

        public AccountController(
            ITokenService tokenService,
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }
    
        [HttpPost("login")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(TokenDto), "JWT access token returned")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, null, "Wrong username or password")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);        

            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return BadRequest();
            }

            var token = await _tokenService.GetTokenForUserAsync(user);
            return Ok(token);
        }
    }
}