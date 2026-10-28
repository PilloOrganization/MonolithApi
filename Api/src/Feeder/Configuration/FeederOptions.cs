namespace Feeder.Configuration;

public sealed class FeederOptions
{
    public Uri BaseAddress { get; set; } = new("https://localhost:7000");
}
