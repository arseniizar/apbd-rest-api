using Users.Api.Models;

namespace Users.Api.Contracts.Responses;

public class AnimalResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public double Weight { get; set; }
    public string FurColor { get; set; }

    public List<VisitResponse> Visits { get; init; }

    public AnimalResponse(Animal animal) =>
        (Id, Name, Category, Weight, FurColor, Visits) =
        (animal.Id, animal.Name, animal.Category, animal.Weight, animal.FurColor,
            animal.Visits.Select(v => new VisitResponse(v)).ToList());
}