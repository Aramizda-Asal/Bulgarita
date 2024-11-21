using Microsoft.AspNetCore.Mvc;
using bulgarita.Models;
using bulgarita.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;

namespace bulgarita.Controllers;

[ApiController]
[Route("[controller]")]

public class Oturum : ControllerBase
{
    [HttpGet("GirişYap/{kullanıcı_adı}/{parola}")]
    public IActionResult GirişYap(string kullanıcı_adı, string parola)
    {
        Models.Oturum yeni_oturum = OturumFonksiyonları.OturumBaşlat(kullanıcı_adı, parola);

        if (yeni_oturum != null)
        {
            return new JsonResult(Newtonsoft.Json.JsonConvert.SerializeObject(yeni_oturum));
        }
        else
        {
            return null;
        }
    }

    [HttpGet("OturumAçık/{oturum}/{kullanıcı}")]
    public IActionResult OturumAçık(string oturum, string kullanıcı)
    {        
        bool oturum_açık = OturumVT.OturumAçık(kullanıcı, oturum);

        if(oturum_açık)
        {
            Models.Kullanıcı şimdi_kullanan = KullanıcıFonksiyonları.kullanıcıAl_Kimlik(kullanıcı);
            JsonResult yanıt = new JsonResult(Newtonsoft.Json.JsonConvert.SerializeObject(şimdi_kullanan));
            yanıt.StatusCode = 200; //OK
            return yanıt; 
        }
        else
        {
            return new StatusCodeResult(403); //Forbidden
        }
    }

    [HttpPost("OturumKapat/{oturum}/{kullanıcı}")]
    public IActionResult OturumKapat(string oturum, string kullanıcı)
    {
        bool kapandı = OturumFonksiyonları.OturumBitir(kullanıcı, oturum);

        if (kapandı)
        {
            return new StatusCodeResult(200); //OK
        }
        else
        {
            return new StatusCodeResult(422); //Unprocessable Content
        }
    }
}