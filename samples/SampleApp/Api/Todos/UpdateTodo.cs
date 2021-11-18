namespace SampleApp.Api.Todos;

[Produces("application/json")]
[ProducesResponseType(Status200OK, Type = typeof(Todo))]
[ProducesResponseType(Status400BadRequest)]
public record UpdateTodo(ITodoStore store)
    : Put("todos/{id:int}", async (int id, Todo todo) =>
    {
        if (todo == null)
        {
            return Status(Status400BadRequest);
        }

        todo = await store.UpsertAsync(id, todo);

        return Json(todo);
    });
