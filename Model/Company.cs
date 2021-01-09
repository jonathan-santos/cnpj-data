using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public enum CompanyType
{
    Parent = 1,
    Subsidiary = 2
}

public enum Situation
{
    Null = 1,
    Active = 2,
    Suspended = 3,
    Incapable = 4,
    Closed = 8
}

[Table("Companies")]
public class Company
{
    [Key]
    [Required]
    [StringLength(14)]
    public string CNPJ { get; set; }

    public CompanyType Type { get; set; }

    [StringLength(150)]
    public string BusinessName { get; set; }

    [StringLength(55)]
    public string FantasyName { get; set; }

    public double SocialCapital { get; set; }

    public Situation RegistrationSituation { get; set; }

    public DateTime RegistrationSituationDate { get; set; }

    [StringLength(8)]
    public string Cep { get; set; }
}
