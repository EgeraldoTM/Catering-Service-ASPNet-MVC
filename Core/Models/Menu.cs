using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class Menu
{
    public Menu()
    {
        FoodItems = new List<FoodItem>();
    }
    public int Id { get; set; }

    [DataType(DataType.Date)]
    public DateTime Date { get; set; }
    public bool IsDeleted { get; set; }
    public ICollection<FoodItem> FoodItems { get; set; }
}
