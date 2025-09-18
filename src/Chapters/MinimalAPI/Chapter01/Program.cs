using System.Security.Claims;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// for implicit attributes cast example
builder.Services.AddScoped<PeopleService>();

// Configure JsonOptions like we did in Controller using AddJsonOptions() after AddControllers();
// JsonOptions comes from Microsoft.AspNetCore.Http.Json, not from Microsoft.AspNetCore.Mvc
// JsonOptions from Microsoft.AspNetCore.Mvc is valid only for controllers, not for minimalAPI
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.
    JsonOptions>(options =>
{
    options.SerializerOptions.DefaultIgnoreCondition =
        JsonIgnoreCondition.WhenWritingNull;
    options.SerializerOptions.IgnoreReadOnlyProperties
        = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.Run();

// Instance Class
class InstanceHandler
{
    public string Hello() => "Hello Instance handler";
}

public class PeopleService { }
public record Person(string FirstName, string LastName);
