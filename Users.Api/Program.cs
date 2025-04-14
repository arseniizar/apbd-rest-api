// using the minimal approach

using Users.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapAnimalEndpoints();
app.MapVisitEndpoints();

app.Run();

// making an internal class generated for minimal api public
public partial class Program
{
}