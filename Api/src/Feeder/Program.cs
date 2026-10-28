using Api.Models.Requests;
using Feeder.Api;
using Feeder.Configuration;
using Feeder.Database;

namespace Feeder;

public static class Program
{
    public static async Task Main()
    {
        await DatabaseReset.ClearAsync();

        IFeederApiClient client = FeederApiClientFactory.Create(new FeederOptions());

        await client.RegisterUserAsync(new RegisterUserRequest
        {
            Username = "lazar3206",
            Password = "Password1!",
            ConfirmPassword = "Password1!"
        });

        (string token, var defaultAccount) = await client.LoginAsync(new LoginUserRequest
        {
            UsernameOrPhoneOrEmail = "lazar3206",
            Password = "Password1!"
        });

        DateOnly startDate = DateOnly.FromDateTime(DateTime.Today);
        await PopulateOlegAccountAsync(client, token, defaultAccount.EntityKey, startDate);
        await PopulateAnnaAccountAsync(client, token, startDate);
    }

    private static async Task PopulateOlegAccountAsync(
        IFeederApiClient client,
        string token,
        Guid accountKey,
        DateOnly startDate)
    {
        await client.EditAccountAsync(
            accountKey,
            new EditAccountRequest
            {
                FirstName = "Oleg",
                LastName = "Lazarovych"
            },
            token);

        Guid backCourseKey = await client.CreateCourseAsync(
            new CreateCourseRequest
            {
                AccountKey = accountKey,
                Name = "Спина"
            },
            token);

        Guid multipleSclerosisCourseKey = await client.CreateCourseAsync(
            new CreateCourseRequest
            {
                AccountKey = accountKey,
                Name = "Розсіяний Склероз"
            },
            token);

        await client.CreatePrescriptionScheduleAsync(
            new CreatePrescriptionScheduleRequest
            {
                CourseKey = backCourseKey,
                MedicineName = "Кальцій",
                StartDate = startDate,
                DurationInDays = 30,
                DailyDoseTimes = [new TimeOnly(20, 30)]
            },
            token);

        await client.CreatePrescriptionScheduleAsync(
            new CreatePrescriptionScheduleRequest
            {
                CourseKey = multipleSclerosisCourseKey,
                MedicineName = "Вітамін Д",
                StartDate = startDate,
                DurationInDays = 14,
                DailyDoseTimes = [new TimeOnly(8, 0)]
            },
            token);
    }

    private static async Task PopulateAnnaAccountAsync(
        IFeederApiClient client,
        string token,
        DateOnly startDate)
    {
        Guid annaAccountKey = await client.CreateAccountAsync(
            new CreateAccountRequest
            {
                FirstName = "Anna",
                LastName = "Kozakova"
            },
            token);

        Guid handCreamCourseKey = await client.CreateCourseAsync(
            new CreateCourseRequest
            {
                AccountKey = annaAccountKey,
                Name = "Крем для рук"
            },
            token);

        Guid fluCourseKey = await client.CreateCourseAsync(
            new CreateCourseRequest
            {
                AccountKey = annaAccountKey,
                Name = "ГРВІ"
            },
            token);

        await client.CreatePrescriptionScheduleAsync(
            new CreatePrescriptionScheduleRequest
            {
                CourseKey = handCreamCourseKey,
                MedicineName = "Ля Рош",
                StartDate = startDate,
                DurationInDays = 30,
                DailyDoseTimes =
                [
                    new TimeOnly(9, 15),
                    new TimeOnly(14, 30),
                    new TimeOnly(19, 45)
                ]
            },
            token);

        await client.CreatePrescriptionScheduleAsync(
            new CreatePrescriptionScheduleRequest
            {
                CourseKey = fluCourseKey,
                MedicineName = "Називін",
                StartDate = startDate,
                DurationInDays = 5,
                DailyDoseTimes =
                [
                    new TimeOnly(11, 0),
                    new TimeOnly(14, 0),
                    new TimeOnly(18, 0)
                ]
            },
            token);

        await client.CreatePrescriptionScheduleAsync(
            new CreatePrescriptionScheduleRequest
            {
                CourseKey = fluCourseKey,
                MedicineName = "Аквамаріс",
                StartDate = startDate,
                DurationInDays = 14,
                DailyDoseTimes =
                [
                    new TimeOnly(7, 0),
                    new TimeOnly(10, 0),
                    new TimeOnly(13, 0),
                    new TimeOnly(15, 0),
                    new TimeOnly(18, 0),
                    new TimeOnly(21, 0)
                ]
            },
            token);
    }
}
