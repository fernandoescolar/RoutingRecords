using RoutingRecords;
using RoutingRecordsAutoBindingApp.Data;

namespace RoutingRecordsAutoBindingApp.Api
{
	delegate void DeleteTodoDelegate([FromRoute]int id);

	public record DeleteTodo(TodoStore store)
		: Delete("todos/{id:int}", new DeleteTodoDelegate(id =>
		{
			store.Delete(id);
		}));
}