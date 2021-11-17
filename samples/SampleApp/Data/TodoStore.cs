namespace SampleApp.Data;

public class TodoStore : ITodoStore
{
    private readonly Hashtable _todos = new Hashtable();
    private int _counter = 0;

    public Task<IEnumerable<Todo>> GetAllAsync()
        => Task.FromResult(_todos.Values.Cast<Todo>());

    public Task<Todo> GetOneAsync(int id)
        => Task.FromResult(_todos[id] as Todo);

    public Task<Todo> InsertAsync(Todo todo)
    {
        _todos[++_counter] = todo = todo with { Id = _counter };
        return Task.FromResult(todo);
    }

    public Task<Todo> UpsertAsync(int id, Todo todo)
    {
        _todos[id] = todo = todo with { Id = id };
        return Task.FromResult(todo);
    }

    public Task DeleteAsync(int id)
    {
        _todos.Remove(id);
        return Task.CompletedTask;
    }
}
