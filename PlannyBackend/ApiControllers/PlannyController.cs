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
using PlannyBackend.Bll.Interfaces;
using PlannyBackend.Web.Dtos;
using PlannyBackend.Web.WebServices;

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
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(PlannyProposalDto), "Creates a planny proposal owned by the user logged in. Returns created planny proposal.")]
        public async Task<IActionResult> CreatePlanny([FromBody] CreatePlannyProposalDto planny)
        {

            var currentUserId = await _currentUserService.GetCurrentUserId();
            planny.OwnerId = currentUserId;
           
            var plannyEnt = planny.ToEntity();         
            await _plannyService.CreatePlanny(plannyEnt);
            return Ok(planny);
        }
    
        [AllowAnonymous]
        [HttpPost("proposals")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(List<PlannyProposalDto>),
            "Returns list of planny proposals by specified in the query object, or all of them if query object is null. ")]
        public async Task<IActionResult> GetPlannies([FromBody] ProposalQueryDto query = null)
        {
            var plannies = new List<PlannyProposalDto>();
            var currentUserId = await _currentUserService.GetCurrentUserId();
            if (query != null)
            {
                plannies = (await _plannyService.SearchPlannyProposals(query.ToEntity()))
                    .Select(e => new PlannyProposalDto(e)).ToList();
            }

            else
            {
                plannies = (await _plannyService.GetPlannyProposalsOfUser(currentUserId))
                  .Select(e => new PlannyProposalDto(e)).ToList();
            }
            
            return Ok(plannies);
        }
      
        [HttpGet("myproposals")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(List<PlannyProposalDtoWithParticipants>),
           "Returns list of planny proposals by specified in the query object, or all of them if query object is null. ")]
        public async Task<IActionResult> GetMyPlannies()
        {
            var currentUserId = await _currentUserService.GetCurrentUserId();

            var plannies = new List<PlannyProposalDtoWithParticipants>();
            plannies = (await _plannyService.GetPlannyProposalsOfUser(currentUserId))
                  .Select(e => new PlannyProposalDtoWithParticipants(e)).ToList();            

            return Ok(plannies);
        }

        [HttpGet("proposals/{id}")]      
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(PlannyProposalDto), "Returns a planny proposals of given Id.")]
        public async Task<IActionResult> GetPlannyProposal(int id)
        {
            var currentUserId = await _currentUserService.GetCurrentUserId();
            var plannyObj = await _plannyService.GetPlannyProposalById(id);
            var participation = plannyObj.Participations
                .Where(p => p.UserId == currentUserId)
                .FirstOrDefault();

            var planny = new PlannyProposalDto(plannyObj);
            if ( participation == null)
            {
                planny.ParticipationState = "none";
            }
            else
            {
                planny.ParticipationState = participation.State.ToString();
            }

            return Ok(planny);
        }


        //todo legyen post 
        [HttpGet("joinproposal/{id}")]     
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(int), "Succesfully joined planny proposal as a participant. Returns id of proposal.")]
        public async Task<IActionResult> JoinPlannyProposal(int id)
        {
            var currentUserId = await _currentUserService.GetCurrentUserId();
            //todo check, hogy van-e ilyen proposal és tudok-e rá jelentkezni
            await _plannyService.JoinProposal(id, currentUserId);
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

        [HttpPut("proposals/{id}")]
        public async Task<IActionResult> UpdateProposal(int id, [FromBody] CreatePlannyProposalDto planny)
        {
            //TODO
            return Ok();
        }

        [HttpDelete("proposals/{id}")]
        public async Task<IActionResult> DeleteProposal(int id)
        {
            await _plannyService.DeleteProposa(id);
            return Ok("delete succesfull");
        }

        [HttpGet("myparticipations")]
        public async Task<IActionResult> GetMyParticipations()
        {
            var currentUserId = await _currentUserService.GetCurrentUserId();
            var part = (await _plannyService.GetParticipationsForUser(currentUserId))
                .Select(e => new MyParticipationDto(e))
                .ToList();           

            return Ok(part);
        }

    }
}