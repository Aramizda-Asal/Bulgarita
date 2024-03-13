using Microsoft.AspNetCore.Mvc;
using bulgarita.Models;
using bulgarita.Services;

namespace bulgarita.Controllers;

[ApiController]
[Route("[controller]")]

public class Oturum : ControllerBase
{

    [HttpGet("GirişYap")]
    public string[] GirişYap(string kullanıcı_adı, string parola)
    {
        Models.Oturum yeni_oturum = OturumFonksiyonları.OturumBaşlat(kullanıcı_adı, parola);

        if (yeni_oturum != null)
        {
            return yeni_oturum.ToStringArray();
        }
        else
        {
            return null;
        }
    }

}