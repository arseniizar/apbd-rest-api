using Users.Api.Models;

namespace Users.Api.Data;

public class DataStore
{
    public static List<Animal> Animals { get; } = new List<Animal>();
    public static List<Visit> Visits { get; } = new List<Visit>();
}