using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlannyBackend.BLL.Dtos.Profile;
using PlannyBackend.BLL.Interfaces;
using PlannyBackend.Web.WebServices;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Net;
using System.Threading.Tasks;

namespace PlannyBackend.Web.ApiControllers
{
    [Produces("application/json")]
    [Route("api/profiles")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme), SwaggerResponse((int)HttpStatusCode.Unauthorized, null, "You are not authorized")]
    public class ProfilesController : ControllerBase
    {
        private readonly CurrentUserService _currentUserService;
        private readonly IProfileService _profileService;

        public ProfilesController(
            IProfileService profileService,
            CurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
            _profileService = profileService;
        }

        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(ProfileDto), "Returns profile data of a user")]
        public async Task<IActionResult> GetProfile(int id)
        {
            var profile = await _profileService.Get(id);
            if (profile == null)
            {
                return NotFound();
            }
            return Ok(profile);
        }

        [HttpGet("{own}")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(ProfileDto), "Returns own profile data")]
        public async Task<IActionResult> GetMyProfile()
        {
            var currentUserId = await _currentUserService.GetCurrentUserId();
            var profile = await _profileService.Get(currentUserId);
            return Ok(profile);
        }

        [HttpPost("{own/edit}")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(string), "Edit was successful.")]
        public async Task<IActionResult> UpdateProfile(ProfileDto profile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var currentUserId = await _currentUserService.GetCurrentUserId();
            await _profileService.Edit(currentUserId, profile);

            return Ok("Edit was successful.");
        }
    }
}
