using RoutingRecords;
using RoutingRecordsAutoBindingApp.Data;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace RoutingRecordsAutoBindingApp.Api
{
	public record UpdateTodo(TodoStore store)
		: Put("todos/{id:int}", (int id, Todo todo) =>
		{
			if (todo == null)
			{
				return Status(Status400BadRequest);
			}

			store.Upsert(id, todo);

			return Json(todo);
		});
}