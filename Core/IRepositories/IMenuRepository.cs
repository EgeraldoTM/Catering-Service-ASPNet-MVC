using CateringService.Core.IRepositories;
using Core.Models;

namespace Core.IRepositories
{
    public interface IMenuRepository : IRepository<Menu>
    {
        Task<Menu?> GetForToday();
	}
}
