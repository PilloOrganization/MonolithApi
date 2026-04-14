namespace Api.Models.Requests
{
    public class LoginUserRequest
    {
        public string UsernameOrPhoneOrEmail { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}