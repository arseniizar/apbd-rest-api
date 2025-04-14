using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Users.Api.Contracts.Requests;
using Users.Api.Contracts.Responses;
using Xunit;

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
        // First, create an animal.
        var newAnimal = new AnimalCreateRequest("Buddy", "Dog", 10.5, "Brown");
        var animalResponse = await _client.PostAsJsonAsync("/api/animals", newAnimal);
        animalResponse.EnsureSuccessStatusCode();
        var createdAnimal = await animalResponse.Content.ReadFromJsonAsync<AnimalResponse>();

        // Arrange: Create a new visit for the animal.
        var newVisit = new VisitCreateRequest(DateTime.UtcNow, "General checkup", 50.0m);

        // Act: Post the visit
        var response = await _client.PostAsJsonAsync($"/api/animals/{createdAnimal.Id}/visits", newVisit);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var visitResponse = await response.Content.ReadFromJsonAsync<VisitResponse>();
        Assert.NotNull(visitResponse);
        Assert.Equal(createdAnimal.Id, visitResponse.AnimalId);
    }

    [Fact]
    public async Task GetVisits_ReturnsVisitsForAnimal()
    {
        // First, create an animal.
        var newAnimal = new AnimalCreateRequest("Charlie", "Dog", 12.0, "Black");
        var animalResponse = await _client.PostAsJsonAsync("/api/animals", newAnimal);
        animalResponse.EnsureSuccessStatusCode();
        var createdAnimal = await animalResponse.Content.ReadFromJsonAsync<AnimalResponse>();

        // Add two visits for the created animal.
        var visit1 = new VisitCreateRequest(DateTime.UtcNow, "Vaccination", 75.0m);
        var visit2 = new VisitCreateRequest(DateTime.UtcNow.AddDays(1), "Checkup", 50.0m);
        var postVisitResponse1 = await _client.PostAsJsonAsync($"/api/animals/{createdAnimal.Id}/visits", visit1);
        var postVisitResponse2 = await _client.PostAsJsonAsync($"/api/animals/{createdAnimal.Id}/visits", visit2);

        postVisitResponse1.EnsureSuccessStatusCode();
        postVisitResponse2.EnsureSuccessStatusCode();

        // Act: Get the visits for the animal.
        var response = await _client.GetAsync($"/api/animals/{createdAnimal.Id}/visits");

        // Assert
        response.EnsureSuccessStatusCode();
        var visits = await response.Content.ReadFromJsonAsync<List<VisitResponse>>();
        Assert.NotNull(visits);
        Assert.True(visits.Count >= 2, "At least two visits should be returned.");
    }
}