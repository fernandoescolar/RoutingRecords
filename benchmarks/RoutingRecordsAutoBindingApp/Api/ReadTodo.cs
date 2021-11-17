using RoutingRecords;
using RoutingRecordsAutoBindingApp.Data;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace RoutingRecordsAutoBindingApp.Api
{
    public record ReadTodo(TodoStore store)
		: Get("todos/{id:int}", (int id) =>
		{
			var todo = store.GetOne(id);
			if (todo == null)
			{
				return Status(Status404NotFound);
			}

			return Json(todo);
		});
}