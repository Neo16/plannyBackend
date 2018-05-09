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

namespace PlannyBackend.ApiControllers
{
    [Produces("application/json")]
    [Route("api/plannies")]
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
            var currentUserId = _userService.GetCurrentUser().Id;
            planny.OwnerId = currentUserId;

            //todo save planny picture 
            var pictureName = await _fileService.UploadPlannyPicture(planny.Picture);
            var plannyEnt = planny.ToEntity();
            plannyEnt.PictureName = pictureName;

            await _plannyService.CreatePlanny(plannyEnt);
            return Ok(planny);
        }
    
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

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme), SwaggerResponse((int)HttpStatusCode.Unauthorized, null, "You are not authorized")]
        [HttpGet("myproposals")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(List<PlannyProposalDto>),
           "Returns list of planny proposals by specified in the query object, or all of them if query object is null. ")]
        public async Task<IActionResult> GetMyPlannies()
        {
            var currentUserId = (await _userService.GetCurrentUser()).Id;

            var plannies = new List<PlannyProposalDto>();
            plannies = (await _plannyService.GetPlannyProposalsOfUser(currentUserId))
                  .Select(e => new PlannyProposalDto(e)).ToList();            

            return Ok(plannies);
        }

        [HttpGet("proposals/{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(PlannyProposalDto), "Returns a planny proposals of given Id.")]
        public async Task<IActionResult> GetPlannyProposal(int id)
        {
            var planny = await _plannyService.GetPlannyProposalById(id);

            return Ok(new PlannyProposalDto(planny));
        }

        [HttpGet("joinproposal")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(int), "Succesfully joined planny proposal as a participant. Returns id of proposal.")]
        public async Task<IActionResult> JoinPlannyProposal([FromQuery] int proposalId)
        {
            //todo check, hogy van-e ilyen proposal és tudok-e rá jelentkezni

            await _plannyService.JoinProposal(proposalId);
            return Ok(proposalId);
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