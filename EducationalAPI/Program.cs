using EducationalAPI.Data.Context;
using EducationalAPI.Data.DAL;
using EducationalAPI.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Serilog;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
DoSetups(builder);
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors();
app.Use(async (context, next) =>
{
    context.Response.GetTypedHeaders().CacheControl =
        new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
        {
            Public = true,
            MaxAge = TimeSpan.FromSeconds(60)
        };
    context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.Vary] =
        new string[] { "Accept-Encoding" };

    await next();
});
//app.Use(async (context, next) =>
//{
//    try
//    {
//        await next.Invoke(context);
//    }
//    catch (NullReferenceException e)
//    {
//        context.Response.StatusCode = 404;
//        await context.Response.WriteAsJsonAsync("Not Found");
//    }
//    catch (ArgumentNullException e)
//    {
//        context.Response.StatusCode = 404;
//        await context.Response.WriteAsJsonAsync("Not Found");
//    }
//    catch (DbUpdateConcurrencyException e)
//    {
//        context.Response.StatusCode = 500;
//        await context.Response.WriteAsJsonAsync("Try again in a moment, database is busy");
//    }
//    catch (Exception e)
//    {
//        context.Response.StatusCode = 500;
//        await context.Response.WriteAsJsonAsync("Threw unexpected exception");
//    }
//});
app.Run();

void DoSetups(WebApplicationBuilder builder)
{
    builder.Services.AddTransient<ErrorHandlerMiddleware>();
    builder.Services.AddControllers().AddNewtonsoftJson(s =>
    {
        s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    });
    var connectionString = builder.Configuration["EducationalAPI:ConnectionString"];
    builder.Services.AddDbContext<EducationalAPIContext>(options => options.UseSqlServer(connectionString));
    builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Educational API",
            Description = "API to save codecool from angry students",
            Version = "v1"
        });
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme.",
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer",
                    },
                },
             Array.Empty<string>()
            }
        });
        var fileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var filePath = Path.Combine(AppContext.BaseDirectory, fileName);
        options.IncludeXmlComments(filePath);
    });
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    var logger = new LoggerConfiguration().WriteTo.File(builder.Configuration.GetValue<string>("Logging:FilePath")).CreateLogger();
    builder.Logging.ClearProviders();
    builder.Logging.AddSerilog(logger);
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ClockSkew = TimeSpan.Zero
        };
    });
}