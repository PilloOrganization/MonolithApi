namespace Api.Models.Requests
{
    public class LoginUserRequest
    {
        public string UsernameOrPhoneOrEmail { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}