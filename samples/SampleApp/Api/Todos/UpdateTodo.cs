using RoutingRecords;
using SampleApp.Data;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace SampleApp.Api.Todos
{
	public record UpdateTodo(ITodoStore Store)
		: Put("todos/{id:int}", async (req, res) =>
		{
			var id = req.FromRoute<int>("id");
			var todo = await req.FromJsonAsync<Todo>();
			if (todo == null)
			{
				res.Status(Status400BadRequest);
				return;
			}

			todo = await Store.UpsertAsync(id, todo);

			await res.JsonAsync(todo);
		});
}