enum MemberIdentifier
{
    JuridicPerson = 1,
    PhysicalPerson = 2,
    Foreigner = 3
}

class Member
{
    public MemberIdentifier Identifier { get; set; }
    public string Name { get; set; }
    public string CNPJCPF { get; set; }
}
