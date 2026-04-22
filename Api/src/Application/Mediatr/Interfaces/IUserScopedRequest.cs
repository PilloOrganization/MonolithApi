namespace Application.Mediatr.Interfaces
{
    public interface IUserScopedRequest
    {
        long UserId { get; set; }
    }
}
