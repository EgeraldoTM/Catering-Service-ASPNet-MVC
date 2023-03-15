using CateringService.Data;
using CateringService.Infrastructure.Persistence.Repositories.Base;
using Core.IRepositories;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class MenuRepository : Repository<Menu>, IMenuRepository
	{
		public MenuRepository(ApplicationDbContext context) : base(context) { }
		public async Task<Menu?> GetMenu(int id)
		{
			return await _context.Menus.Include(m => m.FoodItems).FirstOrDefaultAsync(m => m.Id == id);
		}
		public async Task<Menu?> GetForToday()
		{
			var menu = await _context.Menus
				.Include(m => m.FoodItems)
				.ThenInclude(f => f.Category)
			  .FirstOrDefaultAsync(m => m.Date.Date == DateTime.Now.Date && m.IsDeleted == false);

			return menu;
		}
	}
}
