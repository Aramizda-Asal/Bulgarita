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

        Models.Roller rol_RolVerici = new Models.Roller(RolVerici_KullanıcıK, "Rol Atayıcı/Alıcı");
        bool YetkiVar = RollerFonksiyonları.SatırVar(rol_RolVerici);
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

        Models.Roller rol_RolVerici = new Models.Roller(RolVerici_KullanıcıK, "Rol Atayıcı/Alıcı");
        bool YetkiVar = RollerFonksiyonları.SatırVar(rol_RolVerici);
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

        Models.Roller rol_RolVerici = new Models.Roller(RolVerici_KullanıcıK, "Rol Atayıcı/Alıcı");
        bool YetkiVar = RollerFonksiyonları.SatırVar(rol_RolVerici);
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

        Models.Roller rol_RolVerici = new Models.Roller(RolVerici_KullanıcıK, "Rol Atayıcı/Alıcı");
        bool YetkiVar = RollerFonksiyonları.SatırVar(rol_RolVerici);
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

        Models.Roller rol_RolVerici = new Models.Roller(RolVerici_KullanıcıK, "Rol Atayıcı/Alıcı");
        bool YetkiVar = RollerFonksiyonları.SatırVar(rol_RolVerici);
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

        Models.Roller rol_RolAlıcı = new Models.Roller(RolAlıcı_KullanıcıK, "Rol Atayıcı/Alıcı");
        bool YetkiVar = RollerFonksiyonları.SatırVar(rol_RolAlıcı);
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

        Models.Roller rol_RolAlıcı = new Models.Roller(RolAlıcı_KullanıcıK, "Rol Atayıcı/Alıcı");
        bool YetkiVar = RollerFonksiyonları.SatırVar(rol_RolAlıcı);
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

        Models.Roller rol_RolAlıcı = new Models.Roller(RolAlıcı_KullanıcıK, "Rol Atayıcı/Alıcı");
        bool YetkiVar = RollerFonksiyonları.SatırVar(rol_RolAlıcı);
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

        Models.Roller rol_RolAlıcı = new Models.Roller(RolAlıcı_KullanıcıK, "Rol Atayıcı/Alıcı");
        bool YetkiVar = RollerFonksiyonları.SatırVar(rol_RolAlıcı);
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

        Models.Roller rol_RolAlıcı = new Models.Roller(RolAlıcı_KullanıcıK, "Rol Atayıcı/Alıcı");
        bool YetkiVar = RollerFonksiyonları.SatırVar(rol_RolAlıcı);
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

    /**
    * <summary>
    * Kullanıcının kendisinin Nokta Ekleyici rolü olup olmadığını
    * sorgulaması için API denetçisi.
    * </summary>
    * <remarks>
    * <para>
    * HTTP POST türünde bir metoddur. Yalnızca durum kodu döndürür.
    * </para>
    * </remarks>
    *
    * <param name="KullanıcıKimliği">Sorgulayan kullanıcının kimliği</param>
    * <param name="OturumKimliği">Sorgunun yapıldığı oturumun kimliği</param>
    *
    * <returns>
    * Kullanıcının oturumu geçerliyse ve Nokta Ekleyici rolü varsa <c>200 OK</c>,
    * oturum geçersizse veya Nokta Ekleyici rolü yoksa <c>403 Forbidden</c>.
    * </returns>
    */
    [HttpPost("NoktaEkleyebilirim")]
    public IActionResult NoktaEkleyebilirim(
            [FromHeader(Name="KULLANICI")] string KullanıcıKimliği,
            [FromHeader(Name="OTURUM")] string OturumKimliği)
    {
        bool oturum_açık = OturumVT.OturumAçık(KullanıcıKimliği, OturumKimliği);

        if (oturum_açık)
        {
            Models.Kullanıcı kullanıcı = KullanıcıFonksiyonları.kullanıcıAl_Kimlik(KullanıcıKimliği);
            bool rol_var = RollerFonksiyonları.NoktaEkleyebilir(kullanıcı);

            if (rol_var)
            {
                return new StatusCodeResult(200); //OK
            }
        }

        return new StatusCodeResult(403); //Forbidden
    }

/**
    * <summary>
    * Kullanıcının kendisinin Nokta Düzenleyici rolü olup olmadığını
    * sorgulaması için API denetçisi.
    * </summary>
    * <remarks>
    * <para>
    * HTTP POST türünde bir metoddur. Yalnızca durum kodu döndürür.
    * </para>
    * </remarks>
    *
    * <param name="KullanıcıKimliği">Sorgulayan kullanıcının kimliği</param>
    * <param name="OturumKimliği">Sorgunun yapıldığı oturumun kimliği</param>
    *
    * <returns>
    * Kullanıcının oturumu geçerliyse ve Nokta Düzenleyici rolü varsa <c>200 OK</c>,
    * oturum geçersizse veya Nokta Düzenleyici rolü yoksa <c>403 Forbidden</c>.
    * </returns>
    */
    [HttpPost("NoktaDüzenleyebilirim")]
    public IActionResult NoktaDüzenleyebilirim(
            [FromHeader(Name="KULLANICI")] string KullanıcıKimliği,
            [FromHeader(Name="OTURUM")] string OturumKimliği)
    {
        bool oturum_açık = OturumVT.OturumAçık(KullanıcıKimliği, OturumKimliği);

        if (oturum_açık)
        {
            Models.Kullanıcı kullanıcı = KullanıcıFonksiyonları.kullanıcıAl_Kimlik(KullanıcıKimliği);
            bool rol_var = RollerFonksiyonları.NoktaDüzenleyebilir(kullanıcı);

            if (rol_var)
            {
                return new StatusCodeResult(200); //OK
            }
        }

        return new StatusCodeResult(403); //Forbidden
    }

    /**
    * <summary>
    * Kullanıcının kendisinin Nokta Silici rolü olup olmadığını
    * sorgulaması için API denetçisi.
    * </summary>
    * <remarks>
    * <para>
    * HTTP POST türünde bir metoddur. Yalnızca durum kodu döndürür.
    * </para>
    * </remarks>
    *
    * <param name="KullanıcıKimliği">Sorgulayan kullanıcının kimliği</param>
    * <param name="OturumKimliği">Sorgunun yapıldığı oturumun kimliği</param>
    *
    * <returns>
    * Kullanıcının oturumu geçerliyse ve Nokta Silici rolü varsa <c>200 OK</c>,
    * oturum geçersizse veya Nokta Silici rolü yoksa <c>403 Forbidden</c>.
    * </returns>
    */
    [HttpPost("NoktaSilebilirim")]
    public IActionResult NoktaSilebilirim(
            [FromHeader(Name="KULLANICI")] string KullanıcıKimliği,
            [FromHeader(Name="OTURUM")] string OturumKimliği)
    {
        bool oturum_açık = OturumVT.OturumAçık(KullanıcıKimliği, OturumKimliği);

        if (oturum_açık)
        {
            Models.Kullanıcı kullanıcı = KullanıcıFonksiyonları.kullanıcıAl_Kimlik(KullanıcıKimliği);
            bool rol_var = RollerFonksiyonları.NoktaSilebilir(kullanıcı);

            if (rol_var)
            {
                return new StatusCodeResult(200); //OK
            }
        }

        return new StatusCodeResult(403); //Forbidden
    }

    /**
    * <summary>
    * Kullanıcının kendisinin Rol Atayıcı/Alıcı rolü olup olmadığını
    * sorgulaması için API denetçisi.
    * </summary>
    * <remarks>
    * <para>
    * HTTP POST türünde bir metoddur. Yalnızca durum kodu döndürür.
    * </para>
    * </remarks>
    *
    * <param name="KullanıcıKimliği">Sorgulayan kullanıcının kimliği</param>
    * <param name="OturumKimliği">Sorgunun yapıldığı oturumun kimliği</param>
    *
    * <returns>
    * Kullanıcının oturumu geçerliyse ve Rol Atayıcı/Alıcı rolü varsa <c>200 OK</c>,
    * oturum geçersizse veya Rol Atayıcı/Alıcı rolü yoksa <c>403 Forbidden</c>.
    * </returns>
    */
    [HttpPost("RolAtayabilirimAlabilirim")]
    public IActionResult RolAtayabilirimAlabilirim(
            [FromHeader(Name="KULLANICI")] string KullanıcıKimliği,
            [FromHeader(Name="OTURUM")] string OturumKimliği)
    {
        bool oturum_açık = OturumVT.OturumAçık(KullanıcıKimliği, OturumKimliği);

        if (oturum_açık)
        {
            Models.Kullanıcı kullanıcı = KullanıcıFonksiyonları.kullanıcıAl_Kimlik(KullanıcıKimliği);
            bool rol_var = RollerFonksiyonları.RolAtayabilirAlabilir(kullanıcı);

            if (rol_var)
            {
                return new StatusCodeResult(200); //OK
            }
        }

        return new StatusCodeResult(403); //Forbidden
    }
}