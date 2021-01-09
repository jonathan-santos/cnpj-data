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
        var cnpj = new CNPJData();
        cnpj.RetrieveDataFromFiles();

        var companiesCount = await Companies.CountAsync();
        var membersCount = await Companies.CountAsync();

        if (companiesCount < cnpj.Companies.Count || membersCount < cnpj.Members.Count)
        {
            Console.WriteLine("Populating Database");
            await Companies.AddRangeAsync(cnpj.Companies);
            await Members.AddRangeAsync(cnpj.Members);
            await SaveChangesAsync();
            Console.WriteLine("Finished populating Database");
        }
    }
}
