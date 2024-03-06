using Microsoft.AspNetCore.Mvc;
using bulgarita.Services;
using bulgarita.Models;
using bulgarita;
using MySql.Data.MySqlClient;
using Google.Protobuf.WellKnownTypes;

namespace bulgarita.Controllers;

[ApiController]
[Route("[controller]")]

public class Kullanıcı: ControllerBase
{
    [HttpPost("Ekleme")]
    public IActionResult Ekleme(string Kullanıcı_Adı, string E_Posta, string Parola, Kullanıcı_tür Tür, string Kimlik)
    {
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
    public IActionResult Düzenleme(string kimlik, string veri_sütunu, string eski_veri, string yeni_veri)
    {
        if(KullanıcıFonksiyonları.VeriGuncelle(kimlik, veri_sütunu, eski_veri, yeni_veri))
        {
            return Ok();
        }
        else
        {
            return BadRequest();
        }
    } 
}