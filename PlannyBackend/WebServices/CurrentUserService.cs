using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PlannyBackend.Model.Identity;
using System.Threading.Tasks;

namespace PlannyBackend.Web.WebServices
{
    public class CurrentUserService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<ApplicationUser> userManager;

        public CurrentUserService(
            IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        public async Task<int> GetCurrentUserId()
        {
            var userClaimsPrincipal = httpContextAccessor.HttpContext.User;
            var user = (await userManager.GetUserAsync(userClaimsPrincipal));
            return user.Id;
        }
    }
}
