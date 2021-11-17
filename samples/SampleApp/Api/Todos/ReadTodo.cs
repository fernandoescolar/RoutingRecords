namespace SampleApp.Api.Todos;

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
