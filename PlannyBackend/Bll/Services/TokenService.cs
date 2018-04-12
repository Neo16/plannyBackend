using PlannyBackend.Bll.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlannyBackend.Dtos.Account;
using PlannyBackend.Models.Identity;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace PlannyBackend.Bll.Services
{
    public class TokenService : ITokenService
    {

        private readonly JwtSecurityTokenHandler _accessTokenHandler;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        private const string tokenyKey = "asd_asd_asd_asd_asd_asd_asd_asd_asd_asd_asd_asd_asd_asd_asd_asd_asd_asd_asd_asd_asd";

        public TokenService(           
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _accessTokenHandler = new JwtSecurityTokenHandler
            {
                TokenLifetimeInMinutes = (int)(TimeSpan.FromDays(1)).TotalMinutes
            };
        }

        public async Task<TokenDto> GetTokenForUserAsync(ApplicationUser user)
        {

            var tokenDescriptor = new SecurityTokenDescriptor()
            {                      
                Issuer = "https://localhost:44381/",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenyKey)), SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity((await _signInManager.CreateUserPrincipalAsync(user)).Claims),
            };

            var tokenHandler = _accessTokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            var accessToken = _accessTokenHandler.WriteToken(tokenHandler);

            var res = await _userManager.SetAuthenticationTokenAsync(user, "Local", "Access", accessToken);

            if (res.Succeeded)
            {
                return new TokenDto
                {
                    AccessToken = accessToken,
                };
            }

            else
            {
                return null;
            }            
        }
    }
}
