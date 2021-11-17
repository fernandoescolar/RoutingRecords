namespace SampleApp.Api.Todos;

public record CreateTodo(ITodoStore store)
    : Post("todos", async (Todo todo) =>
    {
        if (todo == null)
        {
            return Status(Status400BadRequest);
        }

        todo = await store.InsertAsync(todo);

        return Status(Status201Created)
               .Json(new
               {
                   Ref = $"todos/{todo.Id}"
               });
    });
