using RoutingRecords;
using SampleApp.Data;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace SampleApp.Api.Todos
{
	public record ReadTodo(ITodoStore Store)
		: Get("todos/{id:int}", async (req, res) =>
		{
			var id = int.Parse((string)req.RouteValues["id"]);
			var todo = await Store.GetOneAsync(id);
			if (todo == null)
			{
				res.Status(Status404NotFound);
				return;
			}

			await res.JsonAsync(todo);
		});
}