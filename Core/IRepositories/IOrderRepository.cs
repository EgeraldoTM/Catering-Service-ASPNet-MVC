using CateringService.Core.IRepositories;
using Core.Models;

namespace Core.IRepositories
{
	public interface IOrderRepository : IRepository<Order>
	{
		Task<Order?> Get(DateTime? date, string employeeId);
		Task<Order?> GetWithDetails(string employeeId);
	}
}
