using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CateringService.Core.DTOs
{
	public class NewMenuDto
	{
        public DateTime Date { get; set; }
        public List<int>? FoodIds { get; set; }
    }
}
