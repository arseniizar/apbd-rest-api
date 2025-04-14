using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Users.Api.Contracts.Requests;
using Users.Api.Contracts.Responses;

namespace Users.Api.Tests;

public class VisitApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public VisitApiTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task PostVisit_CreatesVisit()
    {
        var newAnimal = new AnimalCreateRequest("Buddy", "Dog", 10.5, "Brown");
        var animalResponse = await _client.PostAsJsonAsync("/api/animals", newAnimal);
        animalResponse.EnsureSuccessStatusCode();
        var createdAnimal = await animalResponse.Content.ReadFromJsonAsync<AnimalResponse>();

        // Arrange
        var newVisit = new VisitCreateRequest(System.DateTime.UtcNow, "General checkup", 50.0m);

        // Act
        var response = await _client.PostAsJsonAsync($"/api/animals/{createdAnimal.Id}/visits", newVisit);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var visitResponse = await response.Content.ReadFromJsonAsync<VisitResponse>();
        Assert.NotNull(visitResponse);
        Assert.Equal(createdAnimal.Id, visitResponse.AnimalId);
    }
}