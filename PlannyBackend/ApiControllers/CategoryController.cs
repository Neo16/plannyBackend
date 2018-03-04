using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlannyBackend.Data;
using PlannyBackend.Interfaces;
using System.Net;
using Swashbuckle.AspNetCore.SwaggerGen;
using PlannyBackend.Models;

namespace PlannyBackend.ApiControllers
{
    [Produces("application/json")]
    [Route("api/categories")]
    public class CategoryController : Controller
    {

        private readonly ICategoryService _catService;
      
        public CategoryController(ICategoryService categoryService)
        {
            _catService = categoryService;
        }

        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(List<Category>), "Return all categories.")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _catService.GetCategories();
            return Ok(categories);
        }
    }
}