using PlannyBackend.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using PlannyBackend.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Http;
using PlannyBackend.DAL;
using System;

namespace PlannyBackend.Services
{
    public class UserService : IUserService
    { 
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IUrlHelper Url;        

        public UserService(
               IActionContextAccessor actionContextAccessor,
               IUrlHelperFactory urlHelperFactory,
                ApplicationDbContext context,
                UserManager<ApplicationUser> userManager,
                SignInManager<ApplicationUser> signInManager,
                IHttpContextAccessor httpContextAccessor
            )
        {
            Url = urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext);           
            _userManager = userManager;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<(bool, string)> RegisterUser(ApplicationUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                //Todo emailküldés 
                //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //var callbackUrl = Url.EmailConfirmationLink(user.Id.ToString(), code, "http");
                //await _emailSender.SendEmailConfirmationAsync(user.Email, callbackUrl);               
                return (true,null);
            }
            else
            {
                return (false,result.Errors.FirstOrDefault()?.Description);
            }            
        }

        //private string EmailConfirmationLink(this IUrlHelper urlHelper, string userId, string code, string scheme)
        //{
        //    return urlHelper.Action(
        //        action: nameof("ActionName"),
        //        controller: "Account",
        //        values: new { userId, code },
        //        protocol: scheme);
        //}
    }
}
