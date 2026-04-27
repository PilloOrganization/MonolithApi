namespace Domain.Repositories.Interfaces
{
    public interface IDoseRepository
    {
        Task UpdateIsTakenAsync(Guid key, bool isTaken);
    }
}
