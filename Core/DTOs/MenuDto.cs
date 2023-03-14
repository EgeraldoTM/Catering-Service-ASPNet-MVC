using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;

namespace CateringService.Core.DTOs
{
	public class MenuDto
	{
		public int Id { get; set; }

		[DataType(DataType.Date)]
		public DateTime Date { get; set; }
		public IEnumerable<FoodItemDto> FoodItems { get; set; }
	}
}
