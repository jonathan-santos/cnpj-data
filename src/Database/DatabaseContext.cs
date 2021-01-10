using System;
using System.Diagnostics;
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

            Console.WriteLine("\nDatabase is empty, populating it with CNPJ data...");
            var sw = Stopwatch.StartNew();
            
            await Companies.AddRangeAsync(cnpj.Companies);
            await Members.AddRangeAsync(cnpj.Members);
            await SaveChangesAsync();

            sw.Stop();
            Console.WriteLine($"Finished populating Database with CNPJ data in {sw.ElapsedMilliseconds/1000}s\n");
        }
    }
}
