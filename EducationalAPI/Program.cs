using EducationalAPI.Data.Context;
using EducationalAPI.Data.DAL;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
Setups(builder);
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void Setups(WebApplicationBuilder builder)
{
    builder.Services.AddControllers().AddNewtonsoftJson(s =>
    {
        s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    });
    var connectionString = builder.Configuration.GetConnectionString("EducationalAPIDb");
    builder.Services.AddDbContext<EducationalAPIContext>(options => options.UseSqlServer(connectionString));
    builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    var logger = new LoggerConfiguration().WriteTo.File(builder.Configuration.GetValue<string>("Logging:FilePath")).CreateLogger();
    builder.Logging.ClearProviders();
    builder.Logging.AddSerilog(logger);
}