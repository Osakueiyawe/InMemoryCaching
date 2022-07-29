using INMemoryCaching.Interfaces;
using INMemoryCaching.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseSerilog((ctx, lc) => lc
.MinimumLevel.Debug()
.WriteTo.Debug()
.WriteTo.File("Log\\Log.txt", rollingInterval: RollingInterval.Day));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IFileUtility, FileUtility>();
builder.Services.AddScoped<IInMemoryUtility, InMemoryUtility>();
builder.Services.AddMemoryCache();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
