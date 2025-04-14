namespace Users.Api.Contracts.Requests;

public class VisitCreateRequest
{
    public DateTime DateOfVisit { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }

    public VisitCreateRequest(DateTime dateOfVisit, string description, decimal price)
    {
        DateOfVisit = dateOfVisit;
        Description = description;
        Price = price;
    }
}