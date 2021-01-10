using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class CNPJController : ControllerBase
{
    DatabaseContext _context;

    public CNPJController(DatabaseContext context)
    {
        _context = context;
    }

    [HttpGet("{cnpj}")]
    public async Task<ActionResult<Company>> GetCompany(string cnpj)
    {
        var company = await _context.Companies.FindAsync(cnpj);

        if(company == null)
            return NotFound();

        return company;
    }
}
