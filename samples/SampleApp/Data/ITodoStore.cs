using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleApp.Data
{
	public interface ITodoStore
	{
		Task<IEnumerable<Todo>> GetAllAsync();

		Task<Todo> GetOneAsync(int id);

		Task<Todo> InsertAsync(Todo todo);

		Task<Todo> UpsertAsync(int id, Todo todo);

		Task DeleteAsync(int id);
	}
}
