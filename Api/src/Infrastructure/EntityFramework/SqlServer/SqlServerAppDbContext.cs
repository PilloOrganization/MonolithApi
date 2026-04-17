using Domain.Models;
using Microsoft.EntityFrameworkCore;

public class SqlServerAppDbContext : DbContext
{
    public SqlServerAppDbContext(DbContextOptions<SqlServerAppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Dose> Doses => Set<Dose>();
    public DbSet<Medicine> Medicines => Set<Medicine>();
    public DbSet<PrescriptionSchedule> PrescriptionSchedules => Set<PrescriptionSchedule>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SqlServerAppDbContext).Assembly);
    }
}