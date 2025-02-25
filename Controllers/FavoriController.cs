using Microsoft.AspNetCore.Mvc;
using bulgarita.Services;

namespace bulgarita.Controllers;

[ApiController]
[Route("[controller]")]

public class Favori : ControllerBase
{
    [HttpPost("FavoriEkle")]
    public IActionResult FavoriEkle(
            [FromHeader(Name="KULLANICI")] string Kullanıcı_Kimliği,
            [FromHeader(Name="OTURUM")] string Oturum_Kimliği,
            [FromBody] string body)
    {
        if(OturumVT.OturumAçık(Kullanıcı_Kimliği, Oturum_Kimliği))
        {
            if(FavorilerFonksiyonları.FavoriEkle(Kullanıcı_Kimliği, body))
            {
                return new StatusCodeResult(201); //Created
            }
            else
            {
                return new StatusCodeResult(422); //Unprocessable Content
            }
        }
        else
        {
            return new StatusCodeResult(403); //Forbidden
        }

    }

    [HttpDelete("FavorilerdenCikar")]
    public IActionResult FavorilerdenCikar(
            [FromHeader(Name="KULLANICI")] string Kullanıcı_Kimliği,
            [FromHeader(Name="OTURUM")] string Oturum_Kimliği,
            [FromBody] string body)
    {
        if(OturumVT.OturumAçık(Kullanıcı_Kimliği, Oturum_Kimliği))
        {
            if(FavorilerFonksiyonları.FavorilerdenCikar(Kullanıcı_Kimliği, body))
            {
                return new StatusCodeResult(201); //Created
            }
            else
            {
                return new StatusCodeResult(422); //Unprocessable Content
            }
        }
        else
        {
            return new StatusCodeResult(403); //Forbidden
        }
    }

    [HttpGet("FavorileriGöster")]
    public IActionResult FavorileriAl(
            [FromHeader(Name ="KULLANICI")] string Kullanıcı_Kimliği, 
            [FromHeader(Name="OTURUM")] string Oturum_Kimliği)
    {
        if(OturumVT.OturumAçık(Kullanıcı_Kimliği, Oturum_Kimliği))
        {
            List<String> Favori_Kimlikleri = FavorilerFonksiyonları.FavorileriAl(Kullanıcı_Kimliği);

            if(Favori_Kimlikleri != null)
            {
                JsonResult yanıt = new JsonResult(Newtonsoft.Json.JsonConvert.SerializeObject(Favori_Kimlikleri));
                yanıt.StatusCode = 200; // OK
                return yanıt;
            }
            else
            {
                return new StatusCodeResult(204); //No Content
            }
        }
        else
        {
            return new StatusCodeResult(403); //Forbidden
        }
    }

    [HttpPost("SatirVarMi")]
    public IActionResult SatirVarMi(
            [FromHeader(Name="KULLANICI")] string Kullanıcı_Kimliği,
            [FromHeader(Name="OTURUM")] string Oturum_Kimliği,
            [FromBody] string body)
    {
        if(OturumVT.OturumAçık(Kullanıcı_Kimliği, Oturum_Kimliği))
        {
            if(FavorilerFonksiyonları.SatırVar(Kullanıcı_Kimliği, body))
            {
                return new StatusCodeResult(200); //OK
            }
            else
            {
                return new StatusCodeResult(400); //Bad Request
            }
        }
        else
        {
            return new StatusCodeResult(403); //Forbidden
        }
    }
}
    