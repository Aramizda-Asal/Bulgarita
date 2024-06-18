using Microsoft.AspNetCore.Mvc;
using bulgarita.Services;
using bulgarita.Models;
using bulgarita;

namespace bulgarita.Controllers;

[ApiController]
[Route("[controller]")]

public class Kullanıcı: ControllerBase
{
    [HttpPost("KullanıcıEkle/{Kullanıcı_Adı}/{E_Posta}/{Parola}")]
    public IActionResult KullanıcıEkle(string Kullanıcı_Adı, string E_Posta, string Parola)
    {
        string Kimlik = KullanıcıFonksiyonları.Yeni_Kimlik();
        Kullanıcı_tür Tür = Kullanıcı_tür.kullanıcı;
        Models.Kullanıcı kullanıcı = new Models.Kullanıcı(Kullanıcı_Adı, E_Posta, Parola, Tür, Kimlik);

        if(KullanıcıFonksiyonları.kullanıcıEkle(kullanıcı))
        {
            return new StatusCodeResult(201); //Created
        }
        else
        {
            return new StatusCodeResult(422); //Unprocessable Content
        }
    }

    [HttpPut("KullanıcıAdıDeğiştir/{kimlik}/{GirilenParola}/{Yeni_KullanıcıAdı}")]
    public IActionResult KullancıAdıDeğiştir(string kimlik, string GirilenParola, string Yeni_KullanıcıAdı)
    {
        if(KullanıcıFonksiyonları.KullanıcıAdıDeğiştir(GirilenParola, kimlik, Yeni_KullanıcıAdı))
        {
            return new StatusCodeResult(200); //OK
        }
        else
        {
            return new StatusCodeResult(403); //Forbidden
        }
    }

    [HttpPut("ParolaDeğiştir/{kimlik}/{GirilenParola}/{Yeni_Parola}")]
    public IActionResult ParolaDeğiştir(string kimlik, string GirilenParola, string Yeni_Parola)
    {
        if(KullanıcıFonksiyonları.ParolaDeğiştir(GirilenParola, kimlik, Yeni_Parola))
        {
            return new StatusCodeResult(200); //OK
        }
        else
        {
            return new StatusCodeResult(403); //Forbidden
        }
    }

    [HttpDelete("KullanıcıSil/{kimlik}")]
    public IActionResult KullanıcıSil(string kimlik)
    {
        if(KullanıcıFonksiyonları.KullanıcıSil(kimlik))
        {
            return new StatusCodeResult(200); //OK
        }
        else
        {
            return new StatusCodeResult(422); //Unprocessable Content
        }
    }
}