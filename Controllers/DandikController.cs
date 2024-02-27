using Microsoft.AspNetCore.Mvc;
using bulgarita.Models;

namespace bulgarita.Controllers;

[ApiController]
[Route("habil")]

public class DandikController : ControllerBase
{
    [HttpGet("{seviye}")]
    public Dandik[] Araba(int seviye)
    {
        Dandik[] dandikDizi = new Dandik[3];
        dandikDizi[0] = new Dandik("habil",0);
        dandikDizi[1] = new Dandik("güneş",1);
        dandikDizi[2] = new Dandik("yusuf",seviye);
        return dandikDizi;
    }
}