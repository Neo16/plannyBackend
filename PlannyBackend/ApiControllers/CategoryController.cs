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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace PlannyBackend.ApiControllers
{
    [Produces("application/json")]
    [Route("api/categories")]
   // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme), SwaggerResponse((int)HttpStatusCode.Unauthorized, null, "You are not authorized")]
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