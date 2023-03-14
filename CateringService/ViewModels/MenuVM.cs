using CateringService.Core.DTOs;

namespace CateringService.Web.ViewModels
{
	public class MenuVM
	{
        public MenuDto MenuDto { get; set; }
        public IEnumerable<string> Categories { get; set; }
    }
}
