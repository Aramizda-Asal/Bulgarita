using Microsoft.AspNetCore.Mvc;
using bulgarita.Services;
using bulgarita.Models;
using bulgarita;

namespace bulgarita.Controllers;

[ApiController]
[Route("[controller]")]

public class Kullanıcı: ControllerBase
{
    [HttpGet("Girme")]
    public IActionResult Girme(string Kullanıcı_Adı, string Parola)
    {
        if(KullanıcıFonksiyonları.Kullanıcı_Girişi(Parola, Kullanıcı_Adı))
        {
            return Ok();
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpPost("Ekleme")]
    public IActionResult Ekleme(string Kullanıcı_Adı, string E_Posta, string Parola, Kullanıcı_tür Tür)
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

    [HttpPut("Düzenleme")]
    public IActionResult Düzenleme(string kimlik, /*string veri_sütunu, string eski_veri, string yeni_veri,*/ string GirilenParola, string Yeni_KullanıcıAdı)
    {
        /*
        if(KullanıcıFonksiyonları.VeriGuncelle(kimlik, veri_sütunu, eski_veri, yeni_veri))
        {
            return Ok();
        }
        else
        {
            return BadRequest();
        }*/
        if(KullanıcıFonksiyonları.KullanıcıAdıDeğiştir(GirilenParola, kimlik, Yeni_KullanıcıAdı))
        {
            return Ok();
        }
        else
        {
            return BadRequest();
        }
    } 

    [HttpDelete("Silme")]
    public IActionResult Silme(string kimlik)
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