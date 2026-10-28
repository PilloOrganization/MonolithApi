using Feeder.Configuration;

namespace Feeder.Api;

public static class FeederApiClientFactory
{
    public static IFeederApiClient Create(FeederOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);

        var httpClient = new HttpClient
        {
            BaseAddress = options.BaseAddress
        };

        return new FeederApiClient(httpClient);
    }
}
