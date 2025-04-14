using Users.Api.Models;

namespace Users.Api.Data;

// acts like a db for Visit and Animal entities, idea from the previous task
public class DataStore
{
    public static List<Animal> Animals { get; } = new List<Animal>();
    public static List<Visit> Visits { get; } = new List<Visit>();
}