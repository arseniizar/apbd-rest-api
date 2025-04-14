using System.Text.Json.Serialization;
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

    // to solve an error during tests
    [JsonConstructor]
    public AnimalResponse(int id, string name, string category, double weight, string furColor,
        List<VisitResponse> visits)
    {
        Id = id;
        Name = name;
        Category = category;
        Weight = weight;
        FurColor = furColor;
        Visits = visits;
    }

    public AnimalResponse(Animal animal) =>
        (Id, Name, Category, Weight, FurColor, Visits) =
        (animal.Id, animal.Name, animal.Category, animal.Weight, animal.FurColor,
            animal.Visits.Select(v => new VisitResponse(v)).ToList());
}