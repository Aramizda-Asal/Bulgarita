using Microsoft.AspNetCore.Mvc;
using bulgarita.Services;

namespace bulgarita.Controllers;

[ApiController]
[Route("[controller]")]

public class Roller : ControllerBase
{
    [HttpPost("RolVer_NoktaEkleyici/{Kullanıcı_Kimliği}")]
    public IActionResult RolVer_NoktaEkleyici(string Kullanıcı_Kimliği)
    {
        Models.Roller roller = new Models.Roller(Kullanıcı_Kimliği, "Nokta Ekleyici");

        if(RollerFonksiyonları.RolVer(roller))
        {
            return new StatusCodeResult(201); //Created
        }
        else
        {
            return new StatusCodeResult(422); //Unprocessable Content
        }
    }

    [HttpPost("RolVer_NoktaDüzenleyici/{Kullanıcı_Kimliği}")]
    public IActionResult RolVer_NoktaDüzenleyici(string Kullanıcı_Kimliği)
    {
        Models.Roller roller = new Models.Roller(Kullanıcı_Kimliği, "Nokta Düzenleyici");

        if(RollerFonksiyonları.RolVer(roller))
        {
            return new StatusCodeResult(201); //Created
        }
        else
        {
            return new StatusCodeResult(422); //Unprocessable Content
        }
    }

    [HttpPost("RolVer_NoktaSilici/{Kullanıcı_Kimliği}")]
    public IActionResult RolVer_NoktaSilici(string Kullanıcı_Kimliği)
    {
        Models.Roller roller = new Models.Roller(Kullanıcı_Kimliği, "Nokta Silici");

        if(RollerFonksiyonları.RolVer(roller))
        {
            return new StatusCodeResult(201); //Created
        }
        else
        {
            return new StatusCodeResult(422); //Unprocessable Content
        }
    }

    [HttpPost("RolVer_RolAtayıcıAlıcı/{Kullanıcı_Kimliği}")]
    public IActionResult RolVer_RolAtayıcıAlıcı(string Kullanıcı_Kimliği)
    {
        Models.Roller roller = new Models.Roller(Kullanıcı_Kimliği, "Rol Atayıcı/Alıcı");

        if(RollerFonksiyonları.RolVer(roller))
        {
            return new StatusCodeResult(201); //Created
        }
        else
        {
            return new StatusCodeResult(422); //Unprocessable Content
        }
    }

    [HttpPost("RolVer_KullanıcıSilici/{Kullanıcı_Kimliği}")]
    public IActionResult RolVer_KullanıcıSilici(string Kullanıcı_Kimliği)
    {
        Models.Roller roller = new Models.Roller(Kullanıcı_Kimliği, "Kullanıcı Silici");

        if(RollerFonksiyonları.RolVer(roller))
        {
            return new StatusCodeResult(201); //Created
        }
        else
        {
            return new StatusCodeResult(422); //Unprocessable Content
        }
    }

    [HttpDelete("RolAl_NoktaEkleyici/{Kullanıcı_Kimliği}")]
    public IActionResult RolAl_NoktaEkleyici(string Kullanıcı_Kimliği)
    {
        Models.Roller roller = new Models.Roller(Kullanıcı_Kimliği, "Nokta Ekleyici");

        if(RollerFonksiyonları.RolAl(roller))
        {
            return new StatusCodeResult(200); //OK
        }
        else
        {
            return new StatusCodeResult(422); //Unprocessable Content
        }
    }

    [HttpDelete("RolAl_NoktaDüzenleyici/{Kullanıcı_Kimliği}")]
    public IActionResult RolAl_NoktaDüzenleyici(string Kullanıcı_Kimliği)
    {
        Models.Roller roller = new Models.Roller(Kullanıcı_Kimliği, "Nokta Düzenleyici");

        if(RollerFonksiyonları.RolAl(roller))
        {
            return new StatusCodeResult(200); //OK
        }
        else
        {
            return new StatusCodeResult(422); //Unprocessable Content
        }
    }

    [HttpDelete("RolAl_NoktaSilici/{Kullanıcı_Kimliği}")]
    public IActionResult RolAl_NoktaSilici(string Kullanıcı_Kimliği)
    {
        Models.Roller roller = new Models.Roller(Kullanıcı_Kimliği, "Nokta Silici");

        if(RollerFonksiyonları.RolAl(roller))
        {
            return new StatusCodeResult(200); //OK
        }
        else
        {
            return new StatusCodeResult(422); //Unprocessable Content
        }
    }

    [HttpDelete("RolAl_RolAtayıcıAlıcı/{Kullanıcı_Kimliği}")]
    public IActionResult RolAl_RolAtayıcıAlıcı(string Kullanıcı_Kimliği)
    {
        Models.Roller roller = new Models.Roller(Kullanıcı_Kimliği, "Rol Atayıcı/Alıcı");

        if(RollerFonksiyonları.RolAl(roller))
        {
            return new StatusCodeResult(200); //OK
        }
        else
        {
            return new StatusCodeResult(422); //Unprocessable Content
        }
    }

    [HttpDelete("RolAl_KullanıcıSilici/{Kullanıcı_Kimliği}")]
    public IActionResult RolAl_KullanıcıSilici(string Kullanıcı_Kimliği)
    {
        Models.Roller roller = new Models.Roller(Kullanıcı_Kimliği, "Kullanıcı Silici");

        if(RollerFonksiyonları.RolAl(roller))
        {
            return new StatusCodeResult(200); //OK
        }
        else
        {
            return new StatusCodeResult(422); //Unprocessable Content
        }
    }
}