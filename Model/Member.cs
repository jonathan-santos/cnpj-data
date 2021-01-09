using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;

public enum MemberIdentifier
{
    JuridicPerson = 1,
    PhysicalPerson = 2,
    Foreigner = 3
}

[Table("Members")]
public class Member
{

    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }   

    [StringLength(14)]
    public string CNPJCPF { get; set; }

    public MemberIdentifier Identifier { get; set; }

    [StringLength(150)]
    public string Name { get; set; }
}
