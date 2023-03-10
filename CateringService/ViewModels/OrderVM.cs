using Core.Models;

namespace CateringService.Web.ViewModels
{
	public class OrderVM
	{
		public Order? Order { get; set; }
		public DateOnly Date { get; set; }
	}
}
