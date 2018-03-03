using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlannyBackend.Data;
using PlannyBackend.Interfaces;

namespace PlannyBackend.ApiControllers
{
    [Produces("application/json")]
    [Route("api/Categories")]
    public class CategoryController : Controller
    {

        private readonly ICategoryService _catService;
      
        public CategoryController(ICategoryService categoryService)
        {
            _catService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _catService.GetCategories();
            return Ok(categories);
        }
    }
}