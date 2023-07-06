﻿namespace SampleApi.Repositories
{
	public interface IRepository<T>
		where T : class
	{
		IEnumerable<T> GetAll();
		T Get(int id);
		void Delete(int id);
		void Update(T obj);

	}
}