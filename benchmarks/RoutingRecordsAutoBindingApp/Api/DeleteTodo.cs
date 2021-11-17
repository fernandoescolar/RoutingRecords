using RoutingRecords;
using RoutingRecordsAutoBindingApp.Data;

namespace RoutingRecordsAutoBindingApp.Api
{
    public record DeleteTodo(TodoStore store)
		: Delete("todos/{id:int}", (int id) =>
		{
			store.Delete(id);
		});
}