using RoutingRecords;
using SampleApp.Data;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace SampleApp.Api.Todos
{
	public record CreateTodo(ITodoStore Store)
		: Post("todos", async (req, res) =>
		{
			var todo = await req.FromJsonAsync<Todo>();
			if (todo == null)
			{
				res.Status(Status400BadRequest);
				return;
			}

			todo = await Store.InsertAsync(todo);

			await res.Status(Status201Created)
					 .JsonAsync(new
					 {
						 Ref = $"todos/{todo.Id}"
					 });
		});
}