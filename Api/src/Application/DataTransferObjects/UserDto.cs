namespace Application.DataTransferObjects
{
    public class UserDto : BaseDto
    {
        public string Username { get; set; } = string.Empty;
        public AccountDto DefaultAccountDto { get; set; } = null!;
    }
}
