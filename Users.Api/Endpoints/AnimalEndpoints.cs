using Users.Api.Contracts.Requests;
using Users.Api.Contracts.Responses;
using Users.Api.Data;
using Users.Api.Models;

namespace Users.Api.Endpoints;

public static class AnimalEndpoints
{
    public static IEndpointRouteBuilder MapAnimalEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/animals", () =>
            Results.Ok(DataStore.Animals));

        routes.MapGet("/api/animals/{id:int}", (int id) =>
        {
            var animal = DataStore.Animals.FirstOrDefault(a => a.Id == id);
            return animal is null ? Results.NotFound() : Results.Ok(animal);
        });

        routes.MapPost("/api/animals", (AnimalCreateRequest req) =>
        {
            var animal = new Animal
            {
                Id = DataStore.Animals.Any() ? DataStore.Animals.Max(a => a.Id) + 1 : 1,
                Name = req.Name,
                Category = req.Category,
                Weight = req.Weight,
                FurColor = req.FurColor
            };
            DataStore.Animals.Add(animal);
            var response = new AnimalResponse(animal);
            return Results.Created($"/api/animals/{animal.Id}", response);
        });

        routes.MapPut("/api/animals/{id:int}", (int id, AnimalUpdateRequest req) =>
        {
            var animal = DataStore.Animals.FirstOrDefault(a => a.Id == id);
            if (animal == null)
                return Results.NotFound();
            animal.Name = req.Name;
            animal.Category = req.Category;
            animal.Weight = req.Weight;
            animal.FurColor = req.FurColor;
            return Results.NoContent();
        });

        routes.MapDelete("/api/animals/{id:int}", (int id) =>
        {
            var animal = DataStore.Animals.FirstOrDefault(a => a.Id == id);
            if (animal == null)
                return Results.NotFound();
            DataStore.Animals.Remove(animal);
            return Results.NoContent();
        });

        return routes;
    }
}