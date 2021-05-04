using RoutingRecords;
using RoutingRecordsApp.Data;
using System.Threading.Tasks;

namespace RoutingRecordsApp.Api.Todos
{
	public record DeleteTodo(TodoStore store)
		: Delete("todos/{id:int}", (req, res) =>
		{
			var id = req.FromRoute<int>("id");

			store.Delete(id);

			return Task.CompletedTask;
		});
}