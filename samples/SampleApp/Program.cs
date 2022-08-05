var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<ITodoStore, TodoStore>();
builder.Services.AddRouteRecords();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.MapRouteRecords();

app.Run();