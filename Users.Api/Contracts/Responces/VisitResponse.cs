using Users.Api.Models;

namespace Users.Api.Contracts.Responces;

public class VisitResponse
{
    public int Id { get; set; }
    public DateTime DateOfVisit { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int AnimalId { get; set; }

    public VisitResponse(Visit visit) =>
        (Id, DateOfVisit, Description, Price, AnimalId) =
        (visit.Id, visit.DateOfVisit, visit.Description, visit.Price, visit.AnimalId);
}