using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;

namespace CateringService.Core.DTOs
{
	public class OrderDto
	{
		public int Id { get; set; }
		public DateTime OrderPlaced { get; set; }
		public string EmployeeId { get; set; } = null!;
		public IEnumerable<OrderDetailDto> OrderDetails { get; set; } = null!;
	}
}
