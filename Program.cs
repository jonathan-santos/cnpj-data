using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

public class Program
{
    public static async Task Main(string[] args)
    {
        await GetCNPJData();
        CreateHostBuilder(args).Build().Run();
    }

    static async Task GetCNPJData()
    {
        if (!File.Exists("Data/DADOS_ABERTOS_CNPJ_01.zip"))
        {
            Console.WriteLine("Data/DADOS_ABERTOS_CNPJ_01.zip File doesn't exist, downloading...");
            using var client = new HttpClient();
            client.Timeout = new TimeSpan(1, 0, 0);

            var result = await client.GetAsync("http://200.152.38.155/CNPJ/DADOS_ABERTOS_CNPJ_01.zip");

            if (result.StatusCode != HttpStatusCode.OK)
                throw new Exception("GET CNPJ Data API Request failed");

            using (var fs = new FileStream("Data/DADOS_ABERTOS_CNPJ_01.zip", FileMode.CreateNew))
                await result.Content.CopyToAsync(fs);
        }

        if (!Directory.Exists("Data/01"))
        {
            Console.WriteLine("Data/DADOS_ABERTOS_CNPJ_01.zip not extracted yet, extracting...");
            ZipFile.ExtractToDirectory("Data/DADOS_ABERTOS_CNPJ_01.zip", "Data/01");
        }
        
        foreach(var file in Directory.GetFiles("./Data/01"))
            ReadCNPJData(file);
    }

    static void ReadCNPJData(string file)
    {
        Console.WriteLine($"Reading {file} contents...");

        var companies = new List<Company>();
        var members = new List<Member>();

        using(var reader = new StreamReader(file))
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line[0] == '1')
                {
                    companies.Add(new Company()
                    {
                        CNPJ = line.Substring(3, 14),
                        Type = (CompanyType) int.Parse(line.Substring(17, 1)),
                        BusinessName = line.Substring(18, 150).Trim(),
                        FantasyName = line.Substring(168, 55).Trim(),
                        SocialCapital = double.Parse(line.Substring(891, 14)),
                        RegistrationSituation = (Situation) int.Parse(line.Substring(223, 2)),
                        RegistrationSituationDate = DateTime.ParseExact(line.Substring(225, 8), "yyyyMMdd", null),
                        Cep = line.Substring(674, 8)
                    });
                }
                else if (line[0] == '2')
                {
                    members.Add(new Member()
                    {
                        Identifier = (MemberIdentifier) int.Parse(line.Substring(17, 1)),
                        Name = line.Substring(18, 150).Trim(),
                        CNPJCPF = line.Substring(168, 14)
                    });
                }
            }
        }

        Console.WriteLine($"Finished reading {file} contents");

        // foreach (var company in companies)
        //     Console.WriteLine($"{company.CNPJ} | {company.BusinessName} | {company.Type}");

        // foreach (var member in members)
        //     Console.WriteLine($"{member.CNPJCPF} | {member.Name} | {member.Identifier}");

    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
