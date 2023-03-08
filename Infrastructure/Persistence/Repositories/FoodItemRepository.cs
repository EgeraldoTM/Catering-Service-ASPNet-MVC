using System.Linq.Expressions;
using CateringService.Data;
using CateringService.Infrastructure.Persistence.Repositories.Base;
using Core.IRepositories;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class FoodItemRepository : Repository<FoodItem>, IFoodItemRepository
	{
        public FoodItemRepository(ApplicationDbContext context) : base(context) { }
		public async Task<IEnumerable<FoodItem>> GetWithCategory(string? filter)
		{
			var foodItems = _context.FoodItems.Include(f => f.Category).Where(f => f.IsDeleted == false).AsQueryable();

			if (!string.IsNullOrEmpty(filter))
				foodItems = foodItems.Where(f => f.Name.Contains(filter));

			return await foodItems.ToListAsync();
		}
	}
}
