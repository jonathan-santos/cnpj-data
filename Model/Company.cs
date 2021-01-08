using System;

enum CompanyType
{
    Parent = 1,
    Subsidiary = 2
}

enum Situation
{
    Null = 1,
    Active = 2,
    Suspended = 3,
    Incapable = 4,
    Closed = 8
}

class Company
{
    public string CNPJ { get; set; }
    public CompanyType Type { get; set; }
    public string BusinessName { get; set; }
    public string FantasyName { get; set; }
    public double SocialCapital { get; set; }
    public Situation RegistrationSituation { get; set; }
    public DateTime RegistrationSituationDate { get; set; }
    public string Cep { get; set; }
}
