namespace Application.DataTransferObjects
{
    public class UserDto : BaseDto
    {
        public string Username { get; set; } = null!;
        public AccountDto DefaultAccountDto { get; set; } = null!;
    }
}
