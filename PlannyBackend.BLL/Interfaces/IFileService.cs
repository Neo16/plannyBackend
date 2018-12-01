using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannyBackend.Bll.Interfaces
{
    public interface IFileService
    {
        Task<string> UploadPicture(IFormFile picture); 

    }
}
