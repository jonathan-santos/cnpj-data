using System;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class CNPJController : ControllerBase
{
    public CNPJController(DatabaseContext dbContext) { }

    [HttpGet]
    public string Get()
    {
        return "Hello";
    }
}
