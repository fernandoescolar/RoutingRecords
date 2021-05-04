using RoutingRecords;
using SampleApp.Data;

namespace SampleApp.Api.Todos
{
	public record DeleteTodo(ITodoStore Store)
		: Delete("todos/{id:int}", (req, res) =>
		{
			var id = req.FromRoute<int>("id");
			return Store.DeleteAsync(id);
		});
}