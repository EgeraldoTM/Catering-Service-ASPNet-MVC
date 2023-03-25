using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CateringService.Core.DTOs;

namespace CateringService.Core.Services
{
    public interface IOrderService
    {
        Task CreateOrder(NewOrderDto orderDto, string employeeId);
        Task EditOrder(int id, NewOrderDto orderDto);
    }
}
