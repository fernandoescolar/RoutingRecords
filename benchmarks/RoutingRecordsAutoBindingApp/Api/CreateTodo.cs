using RoutingRecords;
using RoutingRecordsAutoBindingApp.Data;
using System;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace RoutingRecordsAutoBindingApp.Api
{
	delegate IResponse CreateTodoDelegate([FromBody]Todo todo);

	public record CreateTodo(TodoStore store)
		: Post("todos", new CreateTodoDelegate(todo =>
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