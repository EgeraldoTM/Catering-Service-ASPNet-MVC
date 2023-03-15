using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CateringService.Core.DTOs
{
	public class NewOrderDto
	{
        public IEnumerable<NewOrderDetailDto> Details { get; set; }
    }
}
