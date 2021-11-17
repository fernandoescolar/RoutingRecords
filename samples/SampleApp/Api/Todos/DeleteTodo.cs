namespace SampleApp.Api.Todos;

public record DeleteTodo(ITodoStore store)
    : Delete("todos/{id:int}", (int id) =>
        store.DeleteAsync(id)
    );
