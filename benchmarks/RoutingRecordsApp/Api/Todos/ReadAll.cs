using System.Linq;
using RoutingRecords;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace RoutingRecordsApp
{
    public record ReadTodos(TodoStore store)
        : Get("todos", async (req, res) =>
        {
            var todos = store.GetAll();
            if (!todos.Any())
            {
                res.Status(Status204NoContent);
                return;
            }

            await res.JsonAsync(todos);
        });
}