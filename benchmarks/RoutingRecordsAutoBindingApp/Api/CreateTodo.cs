using RoutingRecords;
using RoutingRecordsAutoBindingApp.Data;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace RoutingRecordsAutoBindingApp.Api
{
    public record CreateTodo(TodoStore store)
		: Post("todos", (Todo todo) =>
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
		});
}