using servicebussendsvc;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.Configure<ServiceBus>(builder.Configuration.GetSection(nameof(ServiceBus)));
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
