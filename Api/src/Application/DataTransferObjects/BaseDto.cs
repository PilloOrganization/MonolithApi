namespace Application.DataTransferObjects
{
    public abstract class BaseDto
    {
        public Guid EntityKey { get; set; }
        public bool IsActive { get; set; }
    }
}
