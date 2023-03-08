using CateringService.Core.DTOs;

namespace CateringService.Web.ViewModels
{
	public class ItemWithCategoriesVM
	{
		public FoodItemDto FoodItemDto { get; set; }
		public IEnumerable<CategoryDto> CategoryDtos { get; set; }
	}
}
