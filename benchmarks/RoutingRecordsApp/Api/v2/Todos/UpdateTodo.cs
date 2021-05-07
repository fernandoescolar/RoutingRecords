using RoutingRecords;
using RoutingRecordsApp.Data;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace RoutingRecordsApp.Api.V2.Todos
{
	delegate IResponse UpdateTodoDelegate([FromRoute]int id, [FromBody]Todo todo);

	public record UpdateTodo(TodoStore store)
		: Put("v2/todos/{id:int}", new UpdateTodoDelegate((id, todo) =>
		{
			if (todo == null)
			{
				return Status(Status400BadRequest);
			}

			store.Upsert(id, todo);

			return Json(todo);
		}));
}