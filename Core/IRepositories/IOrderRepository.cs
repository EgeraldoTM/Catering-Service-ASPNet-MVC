using CateringService.Core.IRepositories;
using Core.Models;

namespace Core.IRepositories
{
	public interface IOrderRepository : IRepository<Order>
	{
		Task<Order?> Get(string employeeId, DateTime? date = null);
		Task<Order?> GetWithDetails(string employeeId);
	}
}
