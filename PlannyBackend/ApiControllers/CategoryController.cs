using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlannyBackend.Interfaces;
using System.Net;
using Swashbuckle.AspNetCore.SwaggerGen;
using PlannyBackend.Models;


namespace PlannyBackend.Web.ApiControllers
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

        [Route("sub")]
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(List<Category>), "Return main categories.")]
        public async Task<IActionResult> GetSubCategories()
        {
            var categories = await _catService.GetSubCategories();
            return Ok(categories);
        }


        [Route("main")]
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(List<Category>), "Return main categories.")]
        public async Task<IActionResult> GetMainCategories()
        {
            var categories = await _catService.GetMainCategories();
            return Ok(categories);
        }
    }
}