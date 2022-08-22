using Microsoft.EntityFrameworkCore;
using SimpleWebApi.Data.SQLServer.Context;
using SimpleWebApi.Extensions;
using SimpleWebApi.Filters;
using SimpleWebApi.IoC;
using SimpleWebApi.Services.AutoMapper;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Deal with exceptions using filters
//builder.Services.AddControllers(options =>
//{
//    options.Filters.Add<HttpResponseExceptionFilter>();
//});
// Otherwise use the exception handler midleware
builder.Services.AddControllers().AddJsonOptions(ops =>
{
    ops.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    ops.JsonSerializerOptions.WriteIndented = true;
    ops.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    ops.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
    ops.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
}); ;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(AutoMapperSetup));
builder.Services.AddSwaggerGen();
//builder.Services.AddDbContext<SQLServerContext>(opt => 
//    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerDataBase")).
//        EnableSensitiveDataLogging());
builder.Services.AddDbContext<SQLServerContext>(opt =>
    opt.UseInMemoryDatabase("CoreBank"));

NativeInjector.RegisterServices(builder.Services);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    // Get the instance of BoardGamesDBContext in our services layer
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<SQLServerContext>();

    // Call the DataGenerator to create sample data
    DataGenerator.Initialize(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseExceptionHandler("/error-development");
}
else
{
    app.UseExceptionHandler("/error");
}

app.ConfigureCustomExceptionMiddleware();

app.UseStatusCodePages();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
