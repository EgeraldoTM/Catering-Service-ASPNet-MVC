namespace Core
{
	public interface IUnitOfWork
	{
		Task CompleteAsync();
	}
}
