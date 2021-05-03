using RoutingRecords;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace RoutingRecordsApp
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

            res.Status(Status201Created);
            await res.JsonAsync(new
            {
            Ref = $"todos/{store.Counter}"
            });
        });
}