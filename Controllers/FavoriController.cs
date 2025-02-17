using Microsoft.AspNetCore.Mvc;
using bulgarita.Services;

namespace bulgarita.Controllers;

[ApiController]
[Route("[controller]")]

public class Favori : ControllerBase
{
    [HttpPost("FavoriEkle/{Kullanıcı_Kimliği}/{Konum_Kimliği}")]
    public IActionResult FavoriEkle(string Kullanıcı_Kimliği, string Konum_Kimliği)
    {
        Kullanıcı_Kimliği = Uri.UnescapeDataString(Kullanıcı_Kimliği);
        Konum_Kimliği = Uri.UnescapeDataString(Konum_Kimliği);
        if(FavorilerFonksiyonları.FavoriEkle(Kullanıcı_Kimliği, Konum_Kimliği))
        {
            return new StatusCodeResult(201); //Created
        }
        else
        {
            return new StatusCodeResult(422); //Unprocessable Content
        }
    }

    [HttpDelete("FavorilerdenCikar/{Kullanıcı_Kimliği}/{Konum_Kimliği}")]
    public IActionResult FavorilerdenCikar(string Kullanıcı_Kimliği, string Konum_Kimliği)
    {
        Kullanıcı_Kimliği = Uri.UnescapeDataString(Kullanıcı_Kimliği);
        Konum_Kimliği = Uri.UnescapeDataString(Konum_Kimliği);
        if(FavorilerFonksiyonları.FavorilerdenCikar(Kullanıcı_Kimliği, Konum_Kimliği))
        {
            return new StatusCodeResult(201); //Created
        }
        else
        {
            return new StatusCodeResult(422); //Unprocessable Content
        }
    }

    [HttpPost("SatirVarMi/{Kullanıcı_Kimliği}/{Konum_Kimliği}")]
    public IActionResult SatirVarMi(string Kullanıcı_Kimliği, string Konum_Kimliği)
    {
        Kullanıcı_Kimliği = Uri.UnescapeDataString(Kullanıcı_Kimliği);
        Konum_Kimliği = Uri.UnescapeDataString(Konum_Kimliği);
        if(FavorilerFonksiyonları.SatırVar(Kullanıcı_Kimliği, Konum_Kimliği))
        {
            return new StatusCodeResult(200);
        }
        else
        {
            return new StatusCodeResult(400);
        }
    }

    [HttpGet("FavorileriGöster")]
    public IActionResult FavorileriAl([FromHeader(Name ="KULLANICI")] string Kullanıcı_Kimliği, [FromHeader(Name="OTURUM")] string Oturum_Kimliği)
    {
        bool OturumAçık = OturumVT.OturumAçık(Kullanıcı_Kimliği, Oturum_Kimliği);

        if(OturumAçık)
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
}
    