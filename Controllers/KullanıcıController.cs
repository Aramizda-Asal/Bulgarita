using Microsoft.AspNetCore.Mvc;
using bulgarita.Services;
using bulgarita.Models;
using bulgarita;

namespace bulgarita.Controllers;

[ApiController]
[Route("[controller]")]

public class Kullanıcı: ControllerBase
{
    [HttpPost("KullanıcıEkle")]
    public IActionResult KullanıcıEkle(string Kullanıcı_Adı, string E_Posta, string Parola, Kullanıcı_tür Tür)
    {
        string Kimlik = KullanıcıFonksiyonları.Yeni_Kimlik();
        Models.Kullanıcı kullanıcı = new Models.Kullanıcı(Kullanıcı_Adı, E_Posta, Parola, Tür, Kimlik);

        if(KullanıcıFonksiyonları.kullanıcıEkle(kullanıcı))
        {
            return Ok();
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpPut("KullanıcıAdıDeğiştir")]
    public IActionResult KullancıAdıDeğiştir(string kimlik, string GirilenParola, string Yeni_KullanıcıAdı)
    {
        if(KullanıcıFonksiyonları.KullanıcıAdıDeğiştir(GirilenParola, kimlik, Yeni_KullanıcıAdı))
        {
            return Ok();
        }
        else
        {
            return BadRequest();
        }
    } 

    [HttpPut("ParolaDeğiştir")]
    public IActionResult ParolaDeğiştir(string kimlik, string GirilenParola, string Yeni_Parola)
    {
        if(KullanıcıFonksiyonları.ParolaDeğiştir(GirilenParola, kimlik, Yeni_Parola))
        {
            return Ok();
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpDelete("KullanıcıSil")]
    public IActionResult KullanıcıSil(string kimlik)
    {
        if(KullanıcıFonksiyonları.KullanıcıSil(kimlik))
        {
            return Ok();
        }
        else
        {
            return BadRequest();
        }
    }
}