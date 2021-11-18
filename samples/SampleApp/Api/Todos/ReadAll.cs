namespace SampleApp.Api.Todos;

[Produces("application/json")]
[ProducesResponseType(Status200OK, Type = typeof(Todo[]))]
[ProducesResponseType(Status204NoContent, Type = typeof(Todo[]))]
public record ReadTodos(ITodoStore store)
    : Get("todos", async () =>
    {
        var todos = await store.GetAllAsync();
        if (!todos.Any())
        {
            return Status(Status204NoContent);
        }

        return Json(todos);
    });
