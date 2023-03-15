using CateringService.Core.IRepositories;
using Core.Models;

namespace Core.IRepositories
{
    public interface IMenuRepository : IRepository<Menu>
    {
        Task<Menu?> GetMenu(int id);
        Task<Menu?> GetForToday();
	}
}
