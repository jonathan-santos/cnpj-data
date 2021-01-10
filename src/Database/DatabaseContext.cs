using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class DatabaseContext : DbContext
{
    public DbSet<Company> Companies { get; set; }
    public DbSet<Member> Members { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

    public async Task PopulateDatabaseWithCNPJData()
    {
        var companiesCount = await Companies.CountAsync();
        var membersCount = await Companies.CountAsync();

        if (companiesCount == 0 || membersCount == 0)
        {
            var cnpj = new CNPJData();
            cnpj.RetrieveDataFromFiles();

            Console.WriteLine("Populating Database");
            await Companies.AddRangeAsync(cnpj.Companies);
            await Members.AddRangeAsync(cnpj.Members);
            await SaveChangesAsync();
            Console.WriteLine("Finished populating Database");
        }
    }
}
