namespace Api.Models.Requests
{
    public class TakeDoseRequest
    {
        public Guid DoseKey { get; set; }
        public bool TakeUntake { get; set; }
    }
}
