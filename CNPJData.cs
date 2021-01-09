using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

class CNPJData
{
    static string _dataFolder = "Data";
    static int[] _cnpjDataFilesToDownload = new int[] { 1 };
    static string _uncompressedFilesFolder = "uncompressed";

    public List<Company> Companies { get; set; }
    public List<Member> Members { get; set; }

    public CNPJData()
    {
        Companies = new List<Company>();
        Members = new List<Member>();
    }

    public static async Task PrepareFiles()
    {
        CreateDataFolders();
        await RetrieveDataFiles();
    }

    static void CreateDataFolders()
    {
        if (!Directory.Exists(_dataFolder))
            Directory.CreateDirectory(_dataFolder);

        if (!Directory.Exists($"{_dataFolder}/{_uncompressedFilesFolder}"))
            Directory.CreateDirectory($"{_dataFolder}/{_uncompressedFilesFolder}");
    }

    static async Task RetrieveDataFiles()
    {
        foreach (var fileNumber in _cnpjDataFilesToDownload)
        {
            var file = $"DADOS_ABERTOS_CNPJ_{fileNumber.ToString("D2")}.zip";

            if (!File.Exists($"{_dataFolder}/{file}"))
            {
                Console.WriteLine($"{file} File doesn't exist, downloading...");
                using var client = new HttpClient();
                client.Timeout = new TimeSpan(1, 0, 0);

                var result = await client.GetAsync($"http://200.152.38.155/CNPJ/{file}");

                if (result.StatusCode != HttpStatusCode.OK)
                    throw new Exception("GET CNPJ Data API Request failed");

                using (var fs = new FileStream($"{_dataFolder}/{file}", FileMode.CreateNew))
                    await result.Content.CopyToAsync(fs);
            }

            string dataFile;
            using (var archive = new ZipArchive(File.Open($"{_dataFolder}/{file}", FileMode.Open, FileAccess.Read), ZipArchiveMode.Read))
                dataFile = archive.Entries.First().FullName;

            if (!File.Exists($"{_dataFolder}/{_uncompressedFilesFolder}/{dataFile}"))
            {
                Console.WriteLine($"{file} not extracted yet, extracting...");
                ZipFile.ExtractToDirectory($"{_dataFolder}/{file}", $"{_dataFolder}/{_uncompressedFilesFolder}");
            }
        }
    }

    public void RetrieveDataFromFiles()
    {
        foreach(var file in Directory.GetFiles($"{_dataFolder}/{_uncompressedFilesFolder}"))
            ParseCPNJDataFromFile(file);
    }

    void ParseCPNJDataFromFile(string file)
    {
        Console.WriteLine($"Reading {file} contents...");

        using(var reader = new StreamReader(file))
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                if (line[0] == '1')
                    Companies.Add(ParseCompanyFromLine(line));
                else if (line[0] == '2')
                    Members.Add(ParseMemberFromLine(line));
            }
        }

        Console.WriteLine($"Finished reading {file} contents");
    }

    Company ParseCompanyFromLine(string line)
    {
        return new Company()
        {
            CNPJ = line.Substring(3, 14),
            Type = (CompanyType) int.Parse(line.Substring(17, 1)),
            BusinessName = line.Substring(18, 150).Trim(),
            FantasyName = line.Substring(168, 55).Trim(),
            SocialCapital = double.Parse(line.Substring(891, 14)),
            RegistrationSituation = (Situation) int.Parse(line.Substring(223, 2)),
            RegistrationSituationDate = DateTime.ParseExact(line.Substring(225, 8), "yyyyMMdd", null),
            Cep = line.Substring(674, 8)
        };
    }

    Member ParseMemberFromLine(string line)
    {
        return new Member()
        {
            Identifier = (MemberIdentifier) int.Parse(line.Substring(17, 1)),
            Name = line.Substring(18, 150).Trim(),
            CNPJCPF = line.Substring(168, 14)
        };
    }
}