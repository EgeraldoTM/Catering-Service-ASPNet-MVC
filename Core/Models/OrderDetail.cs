namespace Core.Models;

public class OrderDetail
{
	public int Id { get; set; }
    public byte Quantity { get; set; }
    public decimal Price { get; set; }
    public int OrderId { get; set; }
    public int FoodItemId { get; set; }
    public Order Order { get; set; } = null!;
    public FoodItem FoodItem { get; set; } = null!;
}
