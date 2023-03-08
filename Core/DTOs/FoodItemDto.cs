using System.ComponentModel;

namespace CateringService.Core.DTOs
{
    public class FoodItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        [DisplayName("Category")]
        public byte CategoryId { get; set; }
        public decimal Price { get; set; }
        public CategoryDto? Category { get; set; }
    }
}
