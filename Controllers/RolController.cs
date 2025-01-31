using Microsoft.AspNetCore.Mvc;
using bulgarita.Services;

namespace bulgarita.Controllers;

[ApiController]
[Route("[controller]")]

public class Roller : ControllerBase
{
    [HttpPost("RolVer_NoktaEkleyici/{Kullanıcı_Kimliği}/{RolVerici_KullanıcıK}/{RolVerici_OturumK}")]
    public IActionResult RolVer_NoktaEkleyici(string Kullanıcı_Kimliği, string RolVerici_KullanıcıK, string RolVerici_OturumK)
    {
        Kullanıcı_Kimliği = Uri.UnescapeDataString(Kullanıcı_Kimliği);
        RolVerici_KullanıcıK = Uri.UnescapeDataString(RolVerici_KullanıcıK);
        RolVerici_OturumK = Uri.UnescapeDataString(RolVerici_OturumK);

        bool YetkiVar = RollerFonksiyonları.RolDüzenlemeYetkisineSahip(RolVerici_KullanıcıK);
        bool OturumAçık = OturumVT.OturumAçık(RolVerici_KullanıcıK, RolVerici_OturumK);

        if(YetkiVar && OturumAçık)
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
        else
        {
            return new StatusCodeResult(403); //Forbidden
        }
    }

    [HttpPost("RolVer_NoktaDüzenleyici/{Kullanıcı_Kimliği}/{RolVerici_KullanıcıK}/{RolVerici_OturumK}")]
    public IActionResult RolVer_NoktaDüzenleyici(string Kullanıcı_Kimliği, string RolVerici_KullanıcıK, string RolVerici_OturumK)
    {
        Kullanıcı_Kimliği = Uri.UnescapeDataString(Kullanıcı_Kimliği);
        RolVerici_KullanıcıK = Uri.UnescapeDataString(RolVerici_KullanıcıK);
        RolVerici_OturumK = Uri.UnescapeDataString(RolVerici_OturumK);

        bool YetkiVar = RollerFonksiyonları.RolDüzenlemeYetkisineSahip(RolVerici_KullanıcıK);
        bool OturumAçık = OturumVT.OturumAçık(RolVerici_KullanıcıK, RolVerici_OturumK);

        if(YetkiVar && OturumAçık)
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
        else
        {
            return new StatusCodeResult(403); //Forbidden
        }
    }

    [HttpPost("RolVer_NoktaSilici/{Kullanıcı_Kimliği}/{RolVerici_KullanıcıK}/{RolVerici_OturumK}")]
    public IActionResult RolVer_NoktaSilici(string Kullanıcı_Kimliği, string RolVerici_KullanıcıK, string RolVerici_OturumK)
    {
        Kullanıcı_Kimliği = Uri.UnescapeDataString(Kullanıcı_Kimliği);
        RolVerici_KullanıcıK = Uri.UnescapeDataString(RolVerici_KullanıcıK);
        RolVerici_OturumK = Uri.UnescapeDataString(RolVerici_OturumK);

        bool YetkiVar = RollerFonksiyonları.RolDüzenlemeYetkisineSahip(RolVerici_KullanıcıK);
        bool OturumAçık = OturumVT.OturumAçık(RolVerici_KullanıcıK, RolVerici_OturumK);

        if(YetkiVar && OturumAçık)
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
        else
        {
            return new StatusCodeResult(403); //Forbidden
        }
    }

    [HttpPost("RolVer_RolAtayıcıAlıcı/{Kullanıcı_Kimliği}/{RolVerici_KullanıcıK}/{RolVerici_OturumK}")]
    public IActionResult RolVer_RolAtayıcıAlıcı(string Kullanıcı_Kimliği, string RolVerici_KullanıcıK, string RolVerici_OturumK)
    {
        Kullanıcı_Kimliği = Uri.UnescapeDataString(Kullanıcı_Kimliği);
        RolVerici_KullanıcıK = Uri.UnescapeDataString(RolVerici_KullanıcıK);
        RolVerici_OturumK = Uri.UnescapeDataString(RolVerici_OturumK);

        bool YetkiVar = RollerFonksiyonları.RolDüzenlemeYetkisineSahip(RolVerici_KullanıcıK);
        bool OturumAçık = OturumVT.OturumAçık(RolVerici_KullanıcıK, RolVerici_OturumK);

        if(YetkiVar && OturumAçık)
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
        else
        {
            return new StatusCodeResult(403); //Forbidden
        }
    }

    [HttpPost("RolVer_KullanıcıSilici/{Kullanıcı_Kimliği}/{RolVerici_KullanıcıK}/{RolVerici_OturumK}")]
    public IActionResult RolVer_KullanıcıSilici(string Kullanıcı_Kimliği, string RolVerici_KullanıcıK, string RolVerici_OturumK)
    {
        Kullanıcı_Kimliği = Uri.UnescapeDataString(Kullanıcı_Kimliği);
        RolVerici_KullanıcıK = Uri.UnescapeDataString(RolVerici_KullanıcıK);
        RolVerici_OturumK = Uri.UnescapeDataString(RolVerici_OturumK);

        bool YetkiVar = RollerFonksiyonları.RolDüzenlemeYetkisineSahip(RolVerici_KullanıcıK);
        bool OturumAçık = OturumVT.OturumAçık(RolVerici_KullanıcıK, RolVerici_OturumK);

        if(YetkiVar && OturumAçık)
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
        else
        {
            return new StatusCodeResult(403); //Forbidden
        }
    }

    [HttpDelete("RolAl_NoktaEkleyici/{Kullanıcı_Kimliği}/{RolAlıcı_KullanıcıK}/{RolAlıcı_OturumK}")]
    public IActionResult RolAl_NoktaEkleyici(string Kullanıcı_Kimliği, string RolAlıcı_KullanıcıK, string RolAlıcı_OturumK)
    {
        Kullanıcı_Kimliği = Uri.UnescapeDataString(Kullanıcı_Kimliği);
        RolAlıcı_KullanıcıK = Uri.UnescapeDataString(RolAlıcı_KullanıcıK);
        RolAlıcı_OturumK = Uri.UnescapeDataString(RolAlıcı_OturumK);

        bool YetkiVar = RollerFonksiyonları.RolDüzenlemeYetkisineSahip(RolAlıcı_KullanıcıK);
        bool OturumAçık = OturumVT.OturumAçık(RolAlıcı_KullanıcıK, RolAlıcı_OturumK);

        if(YetkiVar && OturumAçık)
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
        else
        {
            return new StatusCodeResult(403); //Forbidden
        }
    }

    [HttpDelete("RolAl_NoktaDüzenleyici/{Kullanıcı_Kimliği}/{RolAlıcı_KullanıcıK}/{RolAlıcı_OturumK}")]
    public IActionResult RolAl_NoktaDüzenleyici(string Kullanıcı_Kimliği, string RolAlıcı_KullanıcıK, string RolAlıcı_OturumK)
    {
        Kullanıcı_Kimliği = Uri.UnescapeDataString(Kullanıcı_Kimliği);
        RolAlıcı_KullanıcıK = Uri.UnescapeDataString(RolAlıcı_KullanıcıK);
        RolAlıcı_OturumK = Uri.UnescapeDataString(RolAlıcı_OturumK);

        bool YetkiVar = RollerFonksiyonları.RolDüzenlemeYetkisineSahip(RolAlıcı_KullanıcıK);
        bool OturumAçık = OturumVT.OturumAçık(RolAlıcı_KullanıcıK, RolAlıcı_OturumK);

        if(YetkiVar && OturumAçık)
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
        else
        {
            return new StatusCodeResult(403); //Forbidden
        }
    }

    [HttpDelete("RolAl_NoktaSilici/{Kullanıcı_Kimliği}/{RolAlıcı_KullanıcıK}/{RolAlıcı_OturumK}")]
    public IActionResult RolAl_NoktaSilici(string Kullanıcı_Kimliği, string RolAlıcı_KullanıcıK, string RolAlıcı_OturumK)
    {
        Kullanıcı_Kimliği = Uri.UnescapeDataString(Kullanıcı_Kimliği);
        RolAlıcı_KullanıcıK = Uri.UnescapeDataString(RolAlıcı_KullanıcıK);
        RolAlıcı_OturumK = Uri.UnescapeDataString(RolAlıcı_OturumK);

        bool YetkiVar = RollerFonksiyonları.RolDüzenlemeYetkisineSahip(RolAlıcı_KullanıcıK);
        bool OturumAçık = OturumVT.OturumAçık(RolAlıcı_KullanıcıK, RolAlıcı_OturumK);

        if(YetkiVar && OturumAçık)
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
        else
        {
            return new StatusCodeResult(403); //Forbidden
        }
    }

    [HttpDelete("RolAl_RolAtayıcıAlıcı/{Kullanıcı_Kimliği}/{RolAlıcı_KullanıcıK}/{RolAlıcı_OturumK}")]
    public IActionResult RolAl_RolAtayıcıAlıcı(string Kullanıcı_Kimliği, string RolAlıcı_KullanıcıK, string RolAlıcı_OturumK)
    {
        Kullanıcı_Kimliği = Uri.UnescapeDataString(Kullanıcı_Kimliği);
        RolAlıcı_KullanıcıK = Uri.UnescapeDataString(RolAlıcı_KullanıcıK);
        RolAlıcı_OturumK = Uri.UnescapeDataString(RolAlıcı_OturumK);

        bool YetkiVar = RollerFonksiyonları.RolDüzenlemeYetkisineSahip(RolAlıcı_KullanıcıK);
        bool OturumAçık = OturumVT.OturumAçık(RolAlıcı_KullanıcıK, RolAlıcı_OturumK);

        if(YetkiVar && OturumAçık)
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
        else
        {
            return new StatusCodeResult(403); //Forbidden
        }
    }

    [HttpDelete("RolAl_KullanıcıSilici/{Kullanıcı_Kimliği}/{RolAlıcı_KullanıcıK}/{RolAlıcı_OturumK}")]
    public IActionResult RolAl_KullanıcıSilici(string Kullanıcı_Kimliği, string RolAlıcı_KullanıcıK, string RolAlıcı_OturumK)
    {
        Kullanıcı_Kimliği = Uri.UnescapeDataString(Kullanıcı_Kimliği);
        RolAlıcı_KullanıcıK = Uri.UnescapeDataString(RolAlıcı_KullanıcıK);
        RolAlıcı_OturumK = Uri.UnescapeDataString(RolAlıcı_OturumK);

        bool YetkiVar = RollerFonksiyonları.RolDüzenlemeYetkisineSahip(RolAlıcı_KullanıcıK);
        bool OturumAçık = OturumVT.OturumAçık(RolAlıcı_KullanıcıK, RolAlıcı_OturumK);

        if(YetkiVar && OturumAçık)
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
        else
        {
            return new StatusCodeResult(403); //Forbidden
        }
    }
}