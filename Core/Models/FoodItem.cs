using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class FoodItem
{
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public byte CategoryId { get; set; }
    public decimal Price { get; set; }
    public bool IsDeleted { get; set; }
    public Category? Category { get; set; }
    public ICollection<Menu>? Menus { get; set; }
}
