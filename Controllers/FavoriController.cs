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

        if(FavorilerFonksiyonları.SatırVar(Kullanıcı_Kimliği, Konum_Kimliği))
        {
            return new StatusCodeResult(200);
        }
        else
        {
            return new StatusCodeResult(400);
        }
    }
}