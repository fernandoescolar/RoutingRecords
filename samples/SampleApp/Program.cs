var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<ITodoStore, TodoStore>();
builder.Services.AddRouteRecords();

var app = builder.Build();
app.UseHttpsRedirection();
app.MapRouteRecords();


app.Run();