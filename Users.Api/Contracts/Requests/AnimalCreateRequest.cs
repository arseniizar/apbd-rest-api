namespace Users.Api.Contracts.Requests;

public class AnimalCreateRequest
{
    public string Name { get; set; }
    public string Category { get; set; }
    public double Weight { get; set; }
    public string FurColor { get; set; }

    public AnimalCreateRequest(string name, string category, double weight, string furColor)
    {
        Name = name;
        Category = category;
        Weight = weight;
        FurColor = furColor;
    }
}