using System;
using System.Threading.Tasks;
using PlannyBackend.Model.Identity;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using PlannyBackend.Common.Configurations;
using PlannyBackend.Bll.Dtos.Account;

namespace PlannyBackend.Web.WebServices
{
    public class TokenService
    {
        private readonly JwtSecurityTokenHandler _accessTokenHandler;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly TokenConfiguration _tokenConfiguration;      

        public TokenService(           
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IOptions<TokenConfiguration> tokenConfiguration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _accessTokenHandler = new JwtSecurityTokenHandler
            {
                TokenLifetimeInMinutes = (int)(TimeSpan.FromDays(1)).TotalMinutes
            };
            _tokenConfiguration = tokenConfiguration.Value;
        }

        public async Task<string> GetTokenForUserAsync(ApplicationUser user)
        {

            var tokenDescriptor = new SecurityTokenDescriptor()
            {                      
                Issuer = "https://localhost:44381/",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenConfiguration.SigningKey)), SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity((await _signInManager.CreateUserPrincipalAsync(user)).Claims),
            };

            var tokenHandler = _accessTokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            var accessToken = _accessTokenHandler.WriteToken(tokenHandler);

            var res = await _userManager.SetAuthenticationTokenAsync(user, "Local", "Access", accessToken);

            if (res.Succeeded)
            {
                return accessToken;
            }

            else
            {
                return null;
            }            
        }
    }
}
