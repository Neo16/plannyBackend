using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlannyBackend.Interfaces;
using System.Net;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using PlannyBackend.BLL.Interfaces;
using PlannyBackend.BLL.Dtos;
using PlannyBackend.Web.WebServices;
using AutoMapper;
using PlannyBackend.Model;
using PlannyBackend.BLL.Dtos.Plannies.Acquire;

namespace PlannyBackend.ApiControllers
{
    [Produces("application/json")]
    [Route("api/plannies")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme), SwaggerResponse((int)HttpStatusCode.Unauthorized, null, "You are not authorized")]
    public class PlannyController : Controller
    {
        private readonly IPlannyService _plannyService;
        private readonly IUserService _userService;
        private readonly IFileService _fileService;
        private readonly CurrentUserService _currentUserService;

        public PlannyController(
            IFileService fileService,
            IUserService userService,
            IPlannyService plannyService, 
            CurrentUserService currentUserService)
        {
            _fileService = fileService; 
            _userService = userService;
            _plannyService = plannyService;
            _currentUserService = currentUserService;
        }
        
        [HttpPost]      
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(PlannyDto), "Creates a planny owned by the user logged in. Returns created planny.")]
        public async Task<IActionResult> CreatePlanny([FromBody] CreateEditPlannyDto planny)
        {
            var currentUserId = await _currentUserService.GetCurrentUserId();
            planny.OwnerId = currentUserId;            
            await _plannyService.CreatePlanny(planny);
            return Ok(planny);
        }
    
        [AllowAnonymous]
        [HttpPost("search")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(List<PlannyDto>),
            "Returns list of planny by filtered in the query object, or the 30 newest if the query object is null. ")]
        public async Task<IActionResult> SearchPlannies([FromBody] PlannyQueryDto query = null)
        {            
            var plannies = new List<PlannyDto>();
            plannies = await _plannyService.SearchPlannies(query);                                    
            return Ok(plannies);
        }
      
        [HttpGet("myplannies")]     
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(List<PlannyDtoWithParticipations>),
           "Returns list of planny by specified in the query object, or all of them if query object is null. ")]
        public async Task<IActionResult> GetMyPlannies()
        {
            var currentUserId = await _currentUserService.GetCurrentUserId();
            var plannies = new List<PlannyDtoWithParticipations>();
            plannies = (await _plannyService.GetPlanniesOfUser(currentUserId));              
            return Ok(plannies);
        }

        //Todo: legyen külön getPlanny- a manage-hez, és egy getPlannyFor join 
        [HttpGet("{id}")]      
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(PlannyDtoWithJoinStatus), "Returns a planny of given Id.")]
        public async Task<IActionResult> GetPlanny(int id)
        {
            var currentUserId = await _currentUserService.GetCurrentUserId();
            PlannyDtoWithJoinStatus planny = await _plannyService.GetByIdWithJoinStatus(id, currentUserId);
            return Ok(planny);
        }


        //todo legyen post 
        [HttpGet("join/{id}")]     
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(int), "Succesfully joined planny as a participant. Returns id of planny.")]
        public async Task<IActionResult> JoinPlanny(int id)
        {
            var currentUserId = await _currentUserService.GetCurrentUserId();
            //todo check, hogy van-e ilyen és tudok-e rá jelentkezni
            await _plannyService.Join(id, currentUserId);
            return Ok(id);
        }

        
        [HttpPost("cancelparticipation")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(string), "Succesfully canceled to participate.")]
        public async Task<IActionResult> CancelParticiaption([FromBody] int id)
        {
            var currentUserId = await _currentUserService.GetCurrentUserId();
            await _plannyService.CancelParticipation(id, currentUserId);
            return Ok("cancel successful");
        }

        [HttpPost("approveparticipation")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(string), "Successfully approved participation." )]
        public async Task<IActionResult> ApproveParticiaption([FromBody] int id)
        {
            var currentUserId = await _currentUserService.GetCurrentUserId();
            await _plannyService.ApproveParticipation(id, currentUserId);
            return Ok("approve successful");
        }

        [HttpPost("declineparticipation")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(int), "Successfully declined participation.")]
        public async Task<IActionResult> DeclineParticiaption([FromBody] int id)
        {
            var currentUserId = await _currentUserService.GetCurrentUserId();
            await _plannyService.DeclineParticipation(id, currentUserId);
            return Ok("decline successful");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateEditPlannyDto planny)
        {
            var currentUserId = await _currentUserService.GetCurrentUserId();
            await _plannyService.UpdatePlanny(id, planny, currentUserId);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _plannyService.Delete(id);
            return Ok("delete succesfull");
        }

        [HttpGet("myparticipations")]
        public async Task<IActionResult> GetMyParticipations()
        {
            var currentUserId = await _currentUserService.GetCurrentUserId();
            var participations = await _plannyService.GetParticipationsOfUser(currentUserId);                 

            return Ok(participations);
        }

    }
}