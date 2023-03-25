using CateringService.Data;
using CateringService.Infrastructure.Persistence.Repositories.Base;
using Core.IRepositories;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
	public class OrderRepository : Repository<Order>, IOrderRepository
	{
		public OrderRepository(ApplicationDbContext context) : base(context) { }
		public async Task<Order?> Get(string employeeId, DateTime? date = null)
		{
			var filter = date == null ? DateTime.Now.Date : date.Value.Date;

			var order = await _context.Orders
				.Include(o => o.Employee)
				.Include(o => o.OrderDetails)
				.ThenInclude(d => d.FoodItem)
				.Where(o => o.OrderPlaced.Date == filter && o.Employee.Id == employeeId && o.IsDeleted == false)
				.FirstOrDefaultAsync();

			return order;
		}
		public async Task<Order?> GetWithDetails(string employeeId)
		{
			var order = await _context.Orders
				.Include(o => o.OrderDetails)
				.FirstOrDefaultAsync(o => o.OrderPlaced.Date == DateTime.Now.Date && o.EmployeeId == employeeId);

			return order;
		}
	}
}
