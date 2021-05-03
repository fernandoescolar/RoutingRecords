using System.Threading.Tasks;
using RoutingRecords;

namespace SampleApp.Api.Todos
{
    public record DeleteTodo(TodoStore store)
        : Delete("todos/{id:int}", (req, res) =>
        {
            var id = req.FromRoute<int>("id");

            store.Delete(id);

            return Task.CompletedTask;
        });
}