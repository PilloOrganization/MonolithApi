using Api.Models.Requests;
using Application.DataTransferObjects;

namespace Feeder.Api;

public interface IFeederApiClient
{
    Task RegisterUserAsync(RegisterUserRequest request, CancellationToken cancellationToken = default);

    Task<(string Token, AccountDto DefaultAccount)> LoginAsync(
        LoginUserRequest request,
        CancellationToken cancellationToken = default);

    Task<Guid> CreateAccountAsync(
        CreateAccountRequest request,
        string accessToken,
        CancellationToken cancellationToken = default);

    Task EditAccountAsync(
        Guid accountKey,
        EditAccountRequest request,
        string accessToken,
        CancellationToken cancellationToken = default);

    Task<Guid> CreateCourseAsync(
        CreateCourseRequest request,
        string accessToken,
        CancellationToken cancellationToken = default);

    Task<PrescriptionScheduleDto> CreatePrescriptionScheduleAsync(
        CreatePrescriptionScheduleRequest request,
        string accessToken,
        CancellationToken cancellationToken = default);
}
