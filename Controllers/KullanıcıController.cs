using Microsoft.AspNetCore.Mvc;


namespace bulgarita.Controllers;

[ApiController]
[Route("[controller]")]

public class Kullanıcı: ControllerBase
{
    [HttpGet()]
    public char Hello()
    {
        Console.WriteLine("helloo world");
        return 'a';
    }
}