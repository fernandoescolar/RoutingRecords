using RoutingRecords;
using RoutingRecordsApp.Data;
using System;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace RoutingRecordsApp.Api.V2.Todos
{
	delegate IResponse CreateTodoDelegate([FromBody]Todo todo);

	public record CreateTodo(TodoStore store)
		: Post("v2/todos", new CreateTodoDelegate(todo =>
		{
			if (todo == null)
			{
				return Status(Status400BadRequest);
			}

			store.Insert(todo);

			return Status(Status201Created)
				   .Json(new
					{
						Ref = $"todos/{store.Counter}"
					});
		}));
}