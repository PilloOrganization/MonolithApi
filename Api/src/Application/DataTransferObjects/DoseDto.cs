namespace Application.DataTransferObjects
{
    public class DoseDto : BaseDto
    {
        public DateTime Time { get; set; }
        public bool IsTaken { get; set; }
    }
}
