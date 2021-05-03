
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace FirstApproach
{
    public record ReadTodos(TodoStore store)
        : Get("todos", async (req, res) =>
        {
            var todos = store.GetAll();
            if (!todos.Any())
            {
                res.StatusCode = Status204NoContent;
                return;
            }

            await res.JsonAsync(todos);
        });

    public record ReadTodo(TodoStore store)
        : Get("todos/{id:int}", async (req, res) =>
        {
            var id = req.FromRoute<int>("id");
            var todo = store.GetOne(id);
            if (todo == null)
            {
                res.StatusCode = Status400BadRequest;
                return;
            }

            await res.JsonAsync(todo);
        });

    public record CreateTodo(TodoStore store)
        : Post("todos", async (req, res) =>
        {
            var todo = await req.FromJsonAsync<Todo>();
            if (todo == null)
            {
                res.StatusCode = Status400BadRequest;
                return;
            }

            store.Insert(todo);

            res.StatusCode = Status201Created;
            await res.JsonAsync(new { Ref = $"{req.GetDisplayUrl()}/{store.Counter}" });
        });

    public record UpdateTodo(TodoStore store)
        : Put("todos/{id:int}", async (req, res) =>
        {
            var id = req.FromRoute<int>("id");
            var todo = await req.FromJsonAsync<Todo>();
            if (todo == null)
            {
                res.StatusCode = Status400BadRequest;
                return;
            }

            store.Upsert(id, todo);

            await res.JsonAsync(todo);
        });

    public record DeleteTodo(TodoStore store)
        : Delete("todos/{id:int}", (req, res) =>
        {
            var id = req.FromRoute<int>("id");

            store.Delete(id);

            return Task.CompletedTask;
        });
}