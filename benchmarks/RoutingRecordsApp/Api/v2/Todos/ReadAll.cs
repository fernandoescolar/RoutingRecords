using RoutingRecords;
using RoutingRecordsApp.Data;
using System.Linq;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace RoutingRecordsApp.Api.V2.Todos
{
	delegate IResponse ReadTodosDelegate();

	public record ReadTodos(TodoStore store)
		: Get("v2/todos", new ReadTodosDelegate(() =>
		{
			var todos = store.GetAll();
			if (!todos.Any())
			{
				return Status(Status204NoContent);
			}

			return Json(todos);
		}));
}