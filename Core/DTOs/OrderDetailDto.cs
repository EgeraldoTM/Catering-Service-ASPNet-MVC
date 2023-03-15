using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;

namespace CateringService.Core.DTOs
{
	public class OrderDetailDto
	{
		public int Id { get; set; }
		public byte Quantity { get; set; }
		public decimal Price { get; set; }
		public int OrderId { get; set; }
		public int FoodItemId { get; set; }
		public OrderDto Order { get; set; } = null!;
		public FoodItemDto FoodItem { get; set; } = null!;
	}
}
