using RoutingRecords;
using RoutingRecordsApp.Data;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace RoutingRecordsApp.Api.Todos
{
	public record ReadTodo(TodoStore store)
		: Get("todos/{id:int}", async (req, res) =>
		{
			var id = int.Parse((string)req.RouteValues["id"]);
			var todo = store.GetOne(id);
			if (todo == null)
			{
				res.Status(Status404NotFound);
				return;
			}

			await res.JsonAsync(todo);
		});
}