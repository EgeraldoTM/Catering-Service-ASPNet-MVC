using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CateringService.Core.IRepositories
{
	public interface IRepository<TEntity> where TEntity : class
	{
		Task<TEntity?> Get(int id);
		Task<IEnumerable<TEntity>> GetAll();
		Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);
		void Add(TEntity entity);
	}
}
