using CateringService.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace CateringService.Web.Areas.Identity
{
	public class CustomSignInManager : SignInManager<ApplicationUser>
	{
		public CustomSignInManager(UserManager<ApplicationUser> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory, IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<ApplicationUser>> logger, IAuthenticationSchemeProvider schemes, IUserConfirmation<ApplicationUser> confirmation) 
			: base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
		{
		}
		public override async Task<bool> CanSignInAsync(ApplicationUser user)
		{
			if (user.IsDeleted)
				return false;

			return await base.CanSignInAsync(user);
		}
	}
}
