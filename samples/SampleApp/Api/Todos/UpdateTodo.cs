namespace SampleApp.Api.Todos;

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
