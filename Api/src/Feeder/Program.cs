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
        await PopulateIvanAccountAsync(client, token, startDate);
        await PopulateMariaAccountAsync(client, token, startDate);
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

    private static async Task PopulateIvanAccountAsync(
        IFeederApiClient client,
        string token,
        DateOnly startDate)
    {
        Guid ivanAccountKey = await client.CreateAccountAsync(
            new CreateAccountRequest
            {
                FirstName = "Ivan",
                LastName = "Petrov"
            },
            token);

        // Course 1: Diabetes Treatment
        Guid diabetesCourseKey = await client.CreateCourseAsync(
            new CreateCourseRequest
            {
                AccountKey = ivanAccountKey,
                Name = "Diabetes Treatment"
            },
            token);

        // Course 2: Hypertension
        Guid hypertensionCourseKey = await client.CreateCourseAsync(
            new CreateCourseRequest
            {
                AccountKey = ivanAccountKey,
                Name = "Hypertension Control"
            },
            token);

        // Course 3: Cholesterol Management
        Guid cholesterolCourseKey = await client.CreateCourseAsync(
            new CreateCourseRequest
            {
                AccountKey = ivanAccountKey,
                Name = "Cholesterol Management"
            },
            token);

        // Course 4: Thyroid
        Guid thyroidCourseKey = await client.CreateCourseAsync(
            new CreateCourseRequest
            {
                AccountKey = ivanAccountKey,
                Name = "Thyroid Function"
            },
            token);

        // Prescription 1: Metformin - 2 times per day for 90 days
        await client.CreatePrescriptionScheduleAsync(
            new CreatePrescriptionScheduleRequest
            {
                CourseKey = diabetesCourseKey,
                MedicineName = "Metformin",
                StartDate = startDate,
                DurationInDays = 90,
                DailyDoseTimes =
                [
                    new TimeOnly(7, 0),
                    new TimeOnly(19, 0)
                ]
            },
            token);

        // Prescription 2: Lisinopril - 1 time per day for 60 days
        await client.CreatePrescriptionScheduleAsync(
            new CreatePrescriptionScheduleRequest
            {
                CourseKey = hypertensionCourseKey,
                MedicineName = "Lisinopril",
                StartDate = startDate,
                DurationInDays = 60,
                DailyDoseTimes =
                [
                    new TimeOnly(20, 0)
                ]
            },
            token);

        // Prescription 3: Atorvastatin - 1 time per day for 120 days
        await client.CreatePrescriptionScheduleAsync(
            new CreatePrescriptionScheduleRequest
            {
                CourseKey = cholesterolCourseKey,
                MedicineName = "Atorvastatin",
                StartDate = startDate,
                DurationInDays = 120,
                DailyDoseTimes =
                [
                    new TimeOnly(21, 0)
                ]
            },
            token);

        // Prescription 4: Levothyroxine - 1 time per day for 180 days
        await client.CreatePrescriptionScheduleAsync(
            new CreatePrescriptionScheduleRequest
            {
                CourseKey = thyroidCourseKey,
                MedicineName = "Levothyroxine",
                StartDate = startDate,
                DurationInDays = 180,
                DailyDoseTimes =
                [
                    new TimeOnly(6, 30)
                ]
            },
            token);

        // Prescription 5: Aspirin - 1 time per day for 45 days
        await client.CreatePrescriptionScheduleAsync(
            new CreatePrescriptionScheduleRequest
            {
                CourseKey = diabetesCourseKey,
                MedicineName = "Aspirin",
                StartDate = startDate,
                DurationInDays = 45,
                DailyDoseTimes =
                [
                    new TimeOnly(8, 0)
                ]
            },
            token);

        // Prescription 6: Vitamin B12 - 3 times per day for 30 days
        await client.CreatePrescriptionScheduleAsync(
            new CreatePrescriptionScheduleRequest
            {
                CourseKey = hypertensionCourseKey,
                MedicineName = "Vitamin B12",
                StartDate = startDate,
                DurationInDays = 30,
                DailyDoseTimes =
                [
                    new TimeOnly(8, 0),
                    new TimeOnly(12, 30),
                    new TimeOnly(18, 0)
                ]
            },
            token);
    }

    private static async Task PopulateMariaAccountAsync(
        IFeederApiClient client,
        string token,
        DateOnly startDate)
    {
        Guid mariaAccountKey = await client.CreateAccountAsync(
            new CreateAccountRequest
            {
                FirstName = "Maria",
                LastName = "Volkova"
            },
            token);

        // Course 1: Arthritis Treatment
        Guid arthritisCourseKey = await client.CreateCourseAsync(
            new CreateCourseRequest
            {
                AccountKey = mariaAccountKey,
                Name = "Arthritis Treatment"
            },
            token);

        // Course 2: Asthma Management
        Guid asthmaCourseKey = await client.CreateCourseAsync(
            new CreateCourseRequest
            {
                AccountKey = mariaAccountKey,
                Name = "Asthma Management"
            },
            token);

        // Course 3: Vitamin & Supplements
        Guid supplementsCourseKey = await client.CreateCourseAsync(
            new CreateCourseRequest
            {
                AccountKey = mariaAccountKey,
                Name = "Vitamin & Supplements"
            },
            token);

        // Course 4: Bone Density
        Guid boneDensityCourseKey = await client.CreateCourseAsync(
            new CreateCourseRequest
            {
                AccountKey = mariaAccountKey,
                Name = "Bone Density"
            },
            token);

        // Prescription 1: Ibuprofen - 3 times per day for 21 days
        await client.CreatePrescriptionScheduleAsync(
            new CreatePrescriptionScheduleRequest
            {
                CourseKey = arthritisCourseKey,
                MedicineName = "Ibuprofen",
                StartDate = startDate,
                DurationInDays = 21,
                DailyDoseTimes =
                [
                    new TimeOnly(7, 0),
                    new TimeOnly(13, 0),
                    new TimeOnly(19, 0)
                ]
            },
            token);

        // Prescription 2: Albuterol - 2 times per day for 365 days
        await client.CreatePrescriptionScheduleAsync(
            new CreatePrescriptionScheduleRequest
            {
                CourseKey = asthmaCourseKey,
                MedicineName = "Albuterol Inhaler",
                StartDate = startDate,
                DurationInDays = 365,
                DailyDoseTimes =
                [
                    new TimeOnly(8, 0),
                    new TimeOnly(20, 0)
                ]
            },
            token);

        // Prescription 3: Vitamin C - 4 times per day for 90 days
        await client.CreatePrescriptionScheduleAsync(
            new CreatePrescriptionScheduleRequest
            {
                CourseKey = supplementsCourseKey,
                MedicineName = "Vitamin C",
                StartDate = startDate,
                DurationInDays = 90,
                DailyDoseTimes =
                [
                    new TimeOnly(7, 30),
                    new TimeOnly(10, 0),
                    new TimeOnly(13, 30),
                    new TimeOnly(17, 0)
                ]
            },
            token);

        // Prescription 4: Calcium & Magnesium - 2 times per day for 150 days
        await client.CreatePrescriptionScheduleAsync(
            new CreatePrescriptionScheduleRequest
            {
                CourseKey = boneDensityCourseKey,
                MedicineName = "Calcium & Magnesium",
                StartDate = startDate,
                DurationInDays = 150,
                DailyDoseTimes =
                [
                    new TimeOnly(9, 0),
                    new TimeOnly(21, 0)
                ]
            },
            token);

        // Prescription 5: Glucosamine - 3 times per day for 60 days
        await client.CreatePrescriptionScheduleAsync(
            new CreatePrescriptionScheduleRequest
            {
                CourseKey = arthritisCourseKey,
                MedicineName = "Glucosamine",
                StartDate = startDate,
                DurationInDays = 60,
                DailyDoseTimes =
                [
                    new TimeOnly(8, 0),
                    new TimeOnly(12, 0),
                    new TimeOnly(18, 0)
                ]
            },
            token);

        // Prescription 6: Omega-3 - 5 times per day for 45 days
        await client.CreatePrescriptionScheduleAsync(
            new CreatePrescriptionScheduleRequest
            {
                CourseKey = supplementsCourseKey,
                MedicineName = "Omega-3 Fish Oil",
                StartDate = startDate,
                DurationInDays = 45,
                DailyDoseTimes =
                [
                    new TimeOnly(7, 0),
                    new TimeOnly(9, 30),
                    new TimeOnly(12, 30),
                    new TimeOnly(15, 30),
                    new TimeOnly(19, 0)
                ]
            },
            token);
    }
}
