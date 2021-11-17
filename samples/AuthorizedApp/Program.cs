var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRouteRecords();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.Audience = "https://localhost:5001/";
					options.Authority = "https://demo.identityserver.io/";
				});
builder.Services.AddAuthorization();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseAuthentication();
app.UseEndpoints(endpoints => endpoints.MapRouteRecords()
									   .Where(x => x.RouteRecordType.IsNot<Hello>())
									   .ToList()
									   .ForEach(y => y.RequireAuthorization()));


app.Run();