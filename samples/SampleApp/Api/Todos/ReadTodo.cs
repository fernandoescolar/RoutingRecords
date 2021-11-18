namespace SampleApp.Api.Todos;

[Produces("application/json")]
[ProducesResponseType(Status200OK, Type = typeof(Todo))]
[ProducesResponseType(Status404NotFound)]
public record ReadTodo(ITodoStore store)
    : Get("todos/{id:int}", async (int id) =>
    {
        var todo = await store.GetOneAsync(id);
        if (todo == null)
        {
            return Status(Status404NotFound);
        }

        return Json(todo);
    });
