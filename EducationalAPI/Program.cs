using EducationalAPI.Data.Context;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("EducationalAPIDb");
builder.Services.AddDbContext<EducationalAPIContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
Setups(builder);

void Setups(WebApplicationBuilder builder)
{
    var logger = new LoggerConfiguration().WriteTo.File(builder.Configuration.GetValue<string>("Logging:FilePath")).CreateLogger();
    builder.Logging.ClearProviders();
    builder.Logging.AddSerilog(logger);
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
