using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using PlannyBackend.Models.Identity;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Net;
using PlannyBackend.Interfaces;
using PlannyBackend.Web.WebServices;
using PlannyBackend.Web.Dtos.Account;

namespace PlannyBackend.Web.ApiControllers
{
    [Produces("application/json")]
    [Route("api/Account")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly TokenService _tokenService;
        private readonly IUserService _userService;

        public AccountController(
           TokenService tokenService,
           UserManager<ApplicationUser> userManager,
           SignInManager<ApplicationUser> signInManager,
           IUserService userService
          )
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _userService = userService;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(LoginResultDto), "Login result returned")]
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
            return Ok(new LoginResultDto()
            {
                UserName = user.UserName,
                UserToken = token
            });
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
                await _signInManager.SignInAsync(user, isPersistent: false);
                return Ok("Registration sucessful.");
            }
            else
            {
                return BadRequest(error);
            }
        }
    }
}