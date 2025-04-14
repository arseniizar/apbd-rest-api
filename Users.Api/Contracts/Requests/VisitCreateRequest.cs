namespace Users.Api.Contracts.Requests;

public class VisitCreateRequest
{
    public DateTime DateOfVisit { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
}