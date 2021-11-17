using RoutingRecords;
using RoutingRecordsAutoBindingApp.Data;
using System.Linq;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace RoutingRecordsAutoBindingApp.Api
{
    public record ReadTodos(TodoStore store)
		: Get("todos", () =>
		{
			var todos = store.GetAll();
			if (!todos.Any())
			{
				return Status(Status204NoContent);
			}

			return Json(todos);
		});
}