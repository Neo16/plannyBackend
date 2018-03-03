using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlannyBackend.Dtos;
using PlannyBackend.Models;
using PlannyBackend.Interfaces;

namespace PlannyBackend.ApiControllers
{
    [Produces("application/json")]
    [Route("api/planny")]
    public class PlannyController : Controller
    {
        private readonly IPlannyService _plannyService;

        public PlannyController(IPlannyService plannyService)
        {
            _plannyService = plannyService;
        }

        //todo create
        [HttpPost]
        public async Task<IActionResult> CreatePlanny([FromBody] CreatePlannyProposalDto planny)
        {
            // todo: validáció 
            var plannyEnt = planny.ToEntity();
            await _plannyService.CreatePlanny(plannyEnt);
            return Ok();
        }

        //todo list
        [HttpGet("proposals")]
        public async Task<IActionResult> GetPlannies(ProposalQueryDto query = null)
        {
            //if (query != null)
            //{
            //    //todo search 
            //}

            var plannies = await _plannyService.GetPlannyProposals();
            return Ok(plannies);
        }

        //todo get
        [HttpGet("proposals/{id}")]
        public async Task<IActionResult> GetPlannyProposal(int id)
        {
            var planny = await _plannyService.GetPlannyProposalById(id);

            return Ok(new PlannyProposalDto(planny));
        }
    }
}