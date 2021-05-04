using RoutingRecords;
using SampleApp.Data;
using System.Linq;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace SampleApp.Api.Todos
{
	public record ReadTodos(ITodoStore Store)
		: Get("todos", async (req, res) =>
		{
			var todos = await Store.GetAllAsync();
			if (!todos.Any())
			{
				res.Status(Status204NoContent);
				return;
			}

			await res.JsonAsync(todos);
		});
}