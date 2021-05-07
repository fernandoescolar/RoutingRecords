using RoutingRecords;
using RoutingRecordsApp.Data;

namespace RoutingRecordsApp.Api.V2.Todos
{
	delegate void DeleteTodoDelegate([FromRoute]int id);

	public record DeleteTodo(TodoStore store)
		: Delete("v2/todos/{id:int}", new DeleteTodoDelegate(id =>
		{
			store.Delete(id);
		}));
}