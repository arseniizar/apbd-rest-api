namespace Users.Api.Contracts.Requests;

public class AnimalCreateRequest
{
    public string Name { get; set; }
    public string Category { get; set; }
    public double Weight { get; set; }
    public string FurColor { get; set; }
}