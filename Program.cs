using System;
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
        {
            Console.WriteLine($"Reading {file}...");
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
