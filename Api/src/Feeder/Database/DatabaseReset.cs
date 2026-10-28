using System.Text.Json;
using Microsoft.Data.SqlClient;

namespace Feeder.Database;

public static class DatabaseReset
{
    private const string DeleteScript = """
        DELETE FROM Doses;
        DELETE FROM PrescriptionSchedules;
        DELETE FROM Medicines;
        DELETE FROM Courses;
        DELETE FROM Accounts;
        DELETE FROM Users;
        """;

    public static async Task ClearAsync(CancellationToken cancellationToken = default)
    {
        string settingsPath = Path.Combine(AppContext.BaseDirectory, "appsettings.Development.json");
        await using FileStream settingsFile = File.OpenRead(settingsPath);
        using JsonDocument settings = await JsonDocument.ParseAsync(settingsFile, cancellationToken: cancellationToken);

        string connectionString = settings.RootElement
            .GetProperty("ConnectionStrings")
            .GetProperty("DefaultConnection")
            .GetString()
            ?? throw new InvalidOperationException("The development database connection string is missing.");

        await using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        await using var command = new SqlCommand(DeleteScript, connection);
        await command.ExecuteNonQueryAsync(cancellationToken);
    }
}
