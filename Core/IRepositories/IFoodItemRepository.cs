using CateringService.Core.IRepositories;
using Core.Models;

namespace Core.IRepositories
{
    public interface IFoodItemRepository : IRepository<FoodItem>
    {
        Task<IEnumerable<FoodItem>> GetWithCategory(string? filter);
    }
}
