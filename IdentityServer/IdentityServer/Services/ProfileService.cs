using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer.Entities;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Services
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _claimsPrincipalFactory;

        public ProfileService(UserManager<ApplicationUser> userManager , IUserClaimsPrincipalFactory<ApplicationUser> claimsPrincipalFactory)
        {
            _userManager = userManager;
            _claimsPrincipalFactory = claimsPrincipalFactory;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);
            
            context.IssuedClaims.AddRange(context.Subject.Claims);
           
            context.IssuedClaims.Add(new Claim("Org" , "1"));
            
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            return Task.FromResult(0);
        }
    }
}