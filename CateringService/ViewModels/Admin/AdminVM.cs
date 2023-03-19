using Core.Models;

namespace CateringService.Web.ViewModels.Admin
{
	public class AdminVM
	{
		public ApplicationUser User { get; set; }
		public string? Role { get; set; }
	}
}
