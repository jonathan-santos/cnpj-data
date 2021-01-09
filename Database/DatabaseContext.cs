using Microsoft.EntityFrameworkCore;

public class DatabaseContext : DbContext
{
    public DbSet<Company> Companies { get; set; }
    public DbSet<Member> Members { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
}
