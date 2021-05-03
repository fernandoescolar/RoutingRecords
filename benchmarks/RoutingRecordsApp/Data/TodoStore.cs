using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RoutingRecordsApp
{
	public class TodoStore
	{
		private readonly Hashtable _todos = new Hashtable();

		public int Counter { get; private set; }

		public IEnumerable<Todo> GetAll() => _todos.Values.Cast<Todo>().ToList();

		public Todo GetOne(int id) => _todos[id] as Todo;

		public void Insert(Todo todo) => _todos[++Counter] = todo with { Id = Counter };

		public void Upsert(int id, Todo todo) => _todos[id] = todo with { Id = id };

		public void Delete(int id) => _todos.Remove(id);
	}
}
