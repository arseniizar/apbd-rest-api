using System;
using System.Text.Json.Serialization;
using Users.Api.Models;

namespace Users.Api.Contracts.Responses
{
    public class VisitResponse
    {
        public int Id { get; set; }
        public DateTime DateOfVisit { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int AnimalId { get; set; }

        [JsonConstructor]
        public VisitResponse(int id, DateTime dateOfVisit, string description, decimal price, int animalId)
        {
            Id = id;
            DateOfVisit = dateOfVisit;
            Description = description;
            Price = price;
            AnimalId = animalId;
        }

        // convert an entity into a response dto, same in AnimalResponse (often called "conversion constructor" (c) gpt)
        public VisitResponse(Visit visit)
            : this(visit.Id, visit.DateOfVisit, visit.Description, visit.Price, visit.AnimalId)
        {
        }
    }
}