using Core.Models;

namespace CateringService.Web.ViewModels
{
	public class MenuVM
	{
        public Menu Menu { get; set; }
        public IEnumerable<string> Categories { get; set; }
    }
}
