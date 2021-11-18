namespace SampleApp.Api.Todos;

[Produces("application/json")]
[ProducesResponseType(Status200OK)]
public record DeleteTodo(ITodoStore store)
    : Delete("todos/{id:int}", (int id) =>
        store.DeleteAsync(id)
    );
