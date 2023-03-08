using CateringService.Models;

namespace Core.Models;

public class Order
{
    public int Id { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime OrderPlaced { get; set; }
    public bool IsDeleted { get; set; }
    public string EmployeeId { get; set; } = null!;
    public ApplicationUser Employee { get; set; } = null!;
    public ICollection<OrderDetail> OrderDetails { get; set; } = null!;
}
