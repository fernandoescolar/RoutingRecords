namespace SampleApp.Api.Todos;

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
