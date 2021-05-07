using RoutingRecords;
using RoutingRecordsApp.Data;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace RoutingRecordsApp.Api.V2.Todos
{
	delegate IResponse ReadTodoDelegate([FromRoute]int id);

	public record ReadTodo(TodoStore store)
		: Get("v2/todos/{id:int}", new ReadTodoDelegate(id =>
		{
			var todo = store.GetOne(id);
			if (todo == null)
			{
				return Status(Status404NotFound);
			}

			return Json(todo);
		}));
}