namespace SampleApi.Repositories
{
	public interface IRepository<T,U>
		where T : class
	{
		IEnumerable<T> GetAll();
		T Get(int id);
		void Delete(int id);
		void Update(T obj);
		U Add(T obj);

	}
}