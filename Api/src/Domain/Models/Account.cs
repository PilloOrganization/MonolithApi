namespace Domain.Models
{
    public class Account : Entity
    {
        public long UserId { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual List<Course> Courses { get; set; } = new ();
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool IsDefault { get; set; }
        public override void Delete()
        {
            base.Delete();
            foreach (Course course in Courses)
            {
                course.Delete();
            }
        }
    }
}
