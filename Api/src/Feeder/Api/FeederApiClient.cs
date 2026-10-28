using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Api.Models.Requests;
using Application.DataTransferObjects;

namespace Feeder.Api;

public sealed class FeederApiClient(HttpClient httpClient) : IFeederApiClient
{
    public async Task RegisterUserAsync(
        RegisterUserRequest request,
        CancellationToken cancellationToken = default)
    {
        using HttpResponseMessage response = await httpClient.PostAsJsonAsync(
            "api/Users/Register",
            request,
            cancellationToken);

        response.EnsureSuccessStatusCode();
    }

    public async Task<(string Token, AccountDto DefaultAccount)> LoginAsync(
        LoginUserRequest request,
        CancellationToken cancellationToken = default)
    {
        using JsonDocument result = await PostAsync<LoginUserRequest, JsonDocument>(
            "api/Users/Login",
            request,
            cancellationToken) ?? throw new InvalidOperationException("The login response was empty.");

        string token = result.RootElement.GetProperty("token").GetString()
            ?? throw new InvalidOperationException("The login response did not contain a token.");
        AccountDto? defaultAccount = result.RootElement
            .GetProperty("defaultAccount")
            .Deserialize<AccountDto>(JsonSerializerOptions.Web);

        return (token, defaultAccount ?? throw new InvalidOperationException("The login response did not contain a default account."));
    }

    public async Task<Guid> CreateAccountAsync(
        CreateAccountRequest request,
        string accessToken,
        CancellationToken cancellationToken = default)
    {
        using HttpRequestMessage httpRequest = new(HttpMethod.Post, "api/Accounts")
        {
            Content = JsonContent.Create(request)
        };
        httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        using HttpResponseMessage response = await httpClient.SendAsync(httpRequest, cancellationToken);
        response.EnsureSuccessStatusCode();

        JsonElement result = await response.Content.ReadFromJsonAsync<JsonElement>(
            cancellationToken);

        return result.GetProperty("accountKey").GetGuid();
    }

    public async Task EditAccountAsync(
        Guid accountKey,
        EditAccountRequest request,
        string accessToken,
        CancellationToken cancellationToken = default)
    {
        using HttpRequestMessage httpRequest = CreateAuthorizedRequest(
            HttpMethod.Put,
            $"api/Accounts/{accountKey}",
            request,
            accessToken);

        using HttpResponseMessage response = await httpClient.SendAsync(httpRequest, cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    public async Task<Guid> CreateCourseAsync(
        CreateCourseRequest request,
        string accessToken,
        CancellationToken cancellationToken = default)
    {
        using HttpRequestMessage httpRequest = CreateAuthorizedRequest(
            HttpMethod.Post,
            "api/Courses",
            request,
            accessToken);

        using HttpResponseMessage response = await httpClient.SendAsync(httpRequest, cancellationToken);
        response.EnsureSuccessStatusCode();

        JsonElement result = await response.Content.ReadFromJsonAsync<JsonElement>(cancellationToken);
        return result.GetProperty("courseKey").GetGuid();
    }

    public async Task<PrescriptionScheduleDto> CreatePrescriptionScheduleAsync(
        CreatePrescriptionScheduleRequest request,
        string accessToken,
        CancellationToken cancellationToken = default)
    {
        using HttpRequestMessage httpRequest = CreateAuthorizedRequest(
            HttpMethod.Post,
            "api/PrescriptionSchedules",
            request,
            accessToken);

        using HttpResponseMessage response = await httpClient.SendAsync(httpRequest, cancellationToken);
        response.EnsureSuccessStatusCode();

        PrescriptionScheduleDto? result = await response.Content
            .ReadFromJsonAsync<PrescriptionScheduleDto>(cancellationToken);

        return result ?? throw new InvalidOperationException("The prescription schedule response was empty.");
    }

    private static HttpRequestMessage CreateAuthorizedRequest(
        HttpMethod method,
        string requestUri,
        object request,
        string accessToken)
    {
        var httpRequest = new HttpRequestMessage(method, requestUri)
        {
            Content = JsonContent.Create(request)
        };
        httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        return httpRequest;
    }

    private async Task<TResponse?> PostAsync<TRequest, TResponse>(
        string requestUri,
        TRequest request,
        CancellationToken cancellationToken)
    {
        using HttpResponseMessage response = await httpClient.PostAsJsonAsync(
            requestUri,
            request,
            cancellationToken);

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<TResponse>(cancellationToken);
    }
}
