using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CateringService.Core.DTOs
{
	public class NewOrderDetailDto
	{
		public int FoodItemId { get; set; }
		public byte Quantity { get; set; }
		public decimal Price { get; set; }
	}
}
