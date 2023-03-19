using Microsoft.AspNetCore.Mvc.Rendering;

namespace CateringService.Web.ViewModels.Admin
{
    public class IndexVM
    {
		public SelectList Roles { get; set; }
		public List<AdminVM> Users { get; set; }
		public string Filter { get; set; }
		public string RoleFilter { get; set; }
	}
}
