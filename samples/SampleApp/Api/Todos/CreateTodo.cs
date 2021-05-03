using RoutingRecords;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace SampleApp.Api.Todos
{
    public record CreateTodo(TodoStore store)
        : Post("todos", async (req, res) =>
        {
            var todo = await req.FromJsonAsync<Todo>();
            if (todo == null)
            {
                res.Status(Status400BadRequest);
                return;
            }

            store.Insert(todo);

            await res
                    .Status(Status201Created)
                    .JsonAsync(new
                    {
                        Ref = $"todos/{store.Counter}"
                    });
        });
}