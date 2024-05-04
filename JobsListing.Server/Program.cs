using Microsoft.OpenApi.Extensions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "jobs-listing-api", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "jobs-listing-api v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Save the Swagger JSON to a file
var filePath = Path.Combine(app.Environment.ContentRootPath, "swagger.json");
var swaggerProvider = app.Services.GetRequiredService<ISwaggerProvider>();
using (var writer = new StreamWriter(filePath))
{
    var swaggerDoc = swaggerProvider.GetSwagger("v1");
    swaggerDoc.SerializeAsJson(writer.BaseStream, new Microsoft.OpenApi.OpenApiSpecVersion());
}

app.Run();
