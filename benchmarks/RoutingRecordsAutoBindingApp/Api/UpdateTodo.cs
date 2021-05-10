using RoutingRecords;
using RoutingRecordsAutoBindingApp.Data;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace RoutingRecordsAutoBindingApp.Api
{
	delegate IResponse UpdateTodoDelegate([FromRoute]int id, [FromBody]Todo todo);

	public record UpdateTodo(TodoStore store)
		: Put("todos/{id:int}", new UpdateTodoDelegate((id, todo) =>
		{
			if (todo == null)
			{
				return Status(Status400BadRequest);
			}

			store.Upsert(id, todo);

			return Json(todo);
		}));
}