using RoutingRecords;
using RoutingRecordsAutoBindingApp.Data;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace RoutingRecordsAutoBindingApp.Api
{
	delegate IResponse ReadTodoDelegate([FromRoute]int id);

	public record ReadTodo(TodoStore store)
		: Get("todos/{id:int}", new ReadTodoDelegate(id =>
		{
			var todo = store.GetOne(id);
			if (todo == null)
			{
				return Status(Status404NotFound);
			}

			return Json(todo);
		}));
}