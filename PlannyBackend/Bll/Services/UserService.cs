using PlannyBackend.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlannyBackend.Models;
using PlannyBackend.Models.Identity;
using PlannyBackend.Data;
using PlannyBackend.Dtos.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace PlannyBackend.Services
{
    public class UserService : IUserService
    { 
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
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
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        //egyelőre fake, amíg nincs rendes bejelentkezés, addig az első user a current 
        public async Task<ApplicationUser> GetCurrentUser()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            return user;
        }

        public async Task<(bool, string)> RegisterUser(ApplicationUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.EmailConfirmationLink(user.Id.ToString(), code, "http");
                //await _emailSender.SendEmailConfirmationAsync(user.Email, callbackUrl);

                await _signInManager.SignInAsync(user, isPersistent: false);
                return (true,null);
            }
            else
            {
                return (false,result.Errors.FirstOrDefault()?.Description);
            }            
        } 
    }
}
