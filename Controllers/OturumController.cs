// Copyright (C) 2025 Güneş Balcı, Habil Tataroğulları, Yusuf Kozan

using Microsoft.AspNetCore.Mvc;
using bulgarita.Models;
using bulgarita.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;
using System.Web;

namespace bulgarita.Controllers;

[ApiController]
[Route("[controller]")]

public class Oturum : ControllerBase
{
    [HttpGet("GirişYap/{kullanıcı_adı}/{parola}")]
    public IActionResult GirişYap(string kullanıcı_adı, string parola)
    {
        kullanıcı_adı = Uri.UnescapeDataString(kullanıcı_adı);
        parola = Uri.UnescapeDataString(parola);
        Models.Oturum yeni_oturum = OturumFonksiyonları.OturumBaşlat(kullanıcı_adı, parola);

        if (yeni_oturum != null)
        {
            JsonResult yanıt = new JsonResult(Newtonsoft.Json.JsonConvert.SerializeObject(yeni_oturum));
            yanıt.StatusCode = 200; // OK
            return yanıt;
        }
        else
        {
            StatusCodeResult yanıt = new StatusCodeResult(403); // Forbidden
            return yanıt;
        }
    }

    [HttpGet("OturumAçık/{oturum}/{kullanıcı}")]
    public IActionResult OturumAçık(string oturum, string kullanıcı)
    {
        kullanıcı = Uri.UnescapeDataString(kullanıcı);
        oturum = Uri.UnescapeDataString(oturum);

        bool oturum_açık = OturumVT.OturumAçık(kullanıcı, oturum);

        if(oturum_açık)
        {
            Models.Kullanıcı şimdi_kullanan = KullanıcıFonksiyonları.kullanıcıAl_Kimlik(kullanıcı);
            JsonResult yanıt = new JsonResult(Newtonsoft.Json.JsonConvert.SerializeObject(şimdi_kullanan));
            yanıt.StatusCode = 200; // OK
            return yanıt; 
        }
        else
        {
            return new StatusCodeResult(403); // Forbidden
        }
    }

    [HttpPost("OturumKapat/{oturum}/{kullanıcı}")]
    public IActionResult OturumKapat(string oturum, string kullanıcı)
    {
        oturum = Uri.UnescapeDataString(oturum);
        kullanıcı = Uri.UnescapeDataString(kullanıcı);

        bool kapandı = OturumFonksiyonları.OturumBitir(kullanıcı, oturum);

        if (kapandı)
        {
            return new StatusCodeResult(200); // OK
        }
        else
        {
            return new StatusCodeResult(422); // Unprocessable Content
        }
    }
}