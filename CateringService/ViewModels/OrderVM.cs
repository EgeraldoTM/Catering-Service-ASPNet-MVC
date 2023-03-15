using CateringService.Core.DTOs;

namespace CateringService.Web.ViewModels
{
	public class OrderVM
	{
		public OrderDto? OrderDto { get; set; }
		public DateOnly Date { get; set; }
	}
}
