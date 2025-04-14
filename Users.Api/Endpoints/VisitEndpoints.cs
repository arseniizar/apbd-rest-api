using Users.Api.Contracts.Requests;
using Users.Api.Contracts.Responces;
using Users.Api.Data;
using Users.Api.Models;

namespace Users.Api.Endpoints;

public static class VisitEndpoints
{
    public static IEndpointRouteBuilder MapVisitEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/animals/{animalId:int}/visits", (int animalId) =>
        {
            var animal = DataStore.Animals.FirstOrDefault(a => a.Id == animalId);
            if (animal is null)
                return Results.NotFound();
            var animalVisits = DataStore.Visits.Where(v => v.AnimalId == animalId).ToList();
            return Results.Ok(animalVisits);
        });

        routes.MapPost("/api/animals/{animalId:int}/visits", (int animalId, VisitCreateRequest req) =>
        {
            var animal = DataStore.Animals.FirstOrDefault(a => a.Id == animalId);
            if (animal is null)
                return Results.NotFound();
            var visit = new Visit
            {
                Id = DataStore.Visits.Any() ? DataStore.Visits.Max(v => v.Id) + 1 : 1,
                DateOfVisit = req.DateOfVisit,
                Description = req.Description,
                Price = req.Price,
                AnimalId = animalId
            };
            DataStore.Visits.Add(visit);
            animal.Visits.Add(visit);
            var response = new VisitResponse(visit);
            return Results.Created($"/api/animals/{animalId}/visits/{visit.Id}", response);
        });
        return routes;
    }
}