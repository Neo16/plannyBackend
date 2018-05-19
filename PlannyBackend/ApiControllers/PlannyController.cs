using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlannyBackend.Dtos;
using PlannyBackend.Models;
using PlannyBackend.Interfaces;
using System.Net;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using PlannyBackend.Bll.Interfaces;
using PlannyBackend.Models.Enums;

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

        public PlannyController(
            IFileService fileService,
            IUserService userService,
            IPlannyService plannyService)
        {
            _fileService = fileService; 
            _userService = userService;
            _plannyService = plannyService;
        }
        
        [HttpPost]      
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(PlannyProposalDto), "Creates a planny proposal owned by the user logged in. Returns created planny proposal.")]
        public async Task<IActionResult> CreatePlanny([FromBody] CreatePlannyProposalDto planny)
        {
            // todo: validáció            
            var currentUser = await _userService.GetCurrentUser();
            var currentUserId = currentUser.Id;
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
            if (query != null)
            {
                plannies = (await _plannyService.SearchPlannyProposals(query.ToEntity()))
                    .Select(e => new PlannyProposalDto(e)).ToList();
            }

            else
            {
                plannies = (await _plannyService.GetPlannyProposals())
                  .Select(e => new PlannyProposalDto(e)).ToList();
            }
            
            return Ok(plannies);
        }
      
        [HttpGet("myproposals")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(List<PlannyProposalDtoWithParticipants>),
           "Returns list of planny proposals by specified in the query object, or all of them if query object is null. ")]
        public async Task<IActionResult> GetMyPlannies()
        {
            var currentUserId = (await _userService.GetCurrentUser()).Id;

            var plannies = new List<PlannyProposalDtoWithParticipants>();
            plannies = (await _plannyService.GetPlannyProposalsOfUser(currentUserId))
                  .Select(e => new PlannyProposalDtoWithParticipants(e)).ToList();            

            return Ok(plannies);
        }

        [HttpGet("proposals/{id}")]      
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(PlannyProposalDto), "Returns a planny proposals of given Id.")]
        public async Task<IActionResult> GetPlannyProposal(int id)
        {
            var currentUserId = (await _userService.GetCurrentUser()).Id;
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
            //todo check, hogy van-e ilyen proposal és tudok-e rá jelentkezni
            await _plannyService.JoinProposal(id);
            return Ok(id);
        }

        
        [HttpPost("cancelparticipation")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(int), "Succesfully joined planny proposal as a participant. Returns id of proposal.")]
        public async Task<IActionResult> CancelParticiaption([FromBody] int id)
        {           
            await _plannyService.CancelParticipation(id);
            return Ok("cancel successful");
        }


        [HttpGet("approveparticipation/")]       
        public async Task<IActionResult> ApproveParticipation([FromQuery] int proposalId, [FromQuery] int participationId)
        {
            await _plannyService.ApproveParticipation(proposalId, participationId);
            return Ok();
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
            //TODO
            return Ok();
        }

    }
}