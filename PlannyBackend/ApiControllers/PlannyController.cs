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

namespace PlannyBackend.ApiControllers
{
    [Produces("application/json")]
    [Route("api/plannies")]
    public class PlannyController : Controller
    {
        private readonly IPlannyService _plannyService;
        private readonly IUserService _userService;

        public PlannyController(
            IUserService userService,
            IPlannyService plannyService)
        {
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

            var plannyEnt = planny.ToEntity();
            await _plannyService.CreatePlanny(plannyEnt);
            return Ok(planny);
        }

        //todo list
        [HttpGet("proposals")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(List<PlannyProposalDto>),
            "Returns list of planny proposals by specified in the query object, or all of them if query object is null. ")]
        public async Task<IActionResult> GetPlannies(ProposalQueryDto query = null)
        {
            //if (query != null)
            //{
            //    //todo search 
            //}

            var plannies = await _plannyService.GetPlannyProposals();
            return Ok(plannies);
        }
        
        [HttpGet("proposals/{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(PlannyProposalDto), "Returns a planny proposals of given Id.")]
        public async Task<IActionResult> GetPlannyProposal(int id)
        {
            var planny = await _plannyService.GetPlannyProposalById(id);

            return Ok(new PlannyProposalDto(planny));
        }
    }
}