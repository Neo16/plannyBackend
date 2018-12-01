using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using PlannyBackend.Bll.Interfaces;

namespace PlannyBackend.Web.ApiControllers
{
    [Produces("application/json")]
    [Route("api/files")]
    public class FileController : Controller
    {
        private readonly IFileService _fileService;

        public FileController(
            IFileService fileService
            )
        {
            _fileService = fileService;    
        }

        [HttpPost]
       // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme), SwaggerResponse((int)HttpStatusCode.Unauthorized, null, "You are not authorized")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(string), "Upload a cover picture for a planny proposal.")]
        public async Task<IActionResult> UploadPlannyPicture([FromForm] IFormFile Picture)
        {          
            var pictureName = await _fileService.UploadPicture(Picture);
            return Ok(pictureName);
        }      
    }
}