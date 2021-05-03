using RoutingRecords;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace SampleApp.Api.Todos
{
    public record UpdateTodo(TodoStore store)
        : Put("todos/{id:int}", async (req, res) =>
        {
            var id = req.FromRoute<int>("id");
            var todo = await req.FromJsonAsync<Todo>();
            if (todo == null)
            {
                res.Status(Status400BadRequest);
                return;
            }

            store.Upsert(id, todo);

            await res.JsonAsync(todo);
        });
}