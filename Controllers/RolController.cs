// Copyright (C) 2025 Güneş Balcı, Habil Tataroğulları, Yusuf Kozan

using Microsoft.AspNetCore.Mvc;
using bulgarita.Services;
using Newtonsoft.Json;
using Rol_M = bulgarita.Models.Roller;

namespace bulgarita.Controllers;

[ApiController]
[Route("[controller]")]

public class Roller : ControllerBase
{
    [HttpGet("GeçerliRoller")]
    public IActionResult GeçerliRoller()
    {
        try 
        {
            string[] sonuç = Rol_M.RollerDizisi;

            string json = JsonConvert.SerializeObject(sonuç);

            JsonResult yanıt = new JsonResult(json);
            yanıt.StatusCode = 200; //OK
            return yanıt;
        }
        catch
        {
            StatusCodeResult yanıt = new StatusCodeResult(500); //Internal Server Error
            return yanıt;
        }
    }

    [HttpPost("RolVer_NoktaEkleyici/{Kullanıcı_Adı}")]
    public IActionResult RolVer_NoktaEkleyici(string Kullanıcı_Adı,
            [FromHeader(Name="KULLANICI")] string RolVerici_KullanıcıK, 
            [FromHeader(Name="OTURUM")] string RolVerici_OturumK)
    {
        RolVerici_KullanıcıK = Uri.UnescapeDataString(RolVerici_KullanıcıK);
        RolVerici_OturumK = Uri.UnescapeDataString(RolVerici_OturumK);

        Kullanıcı_Adı = Uri.UnescapeDataString(Kullanıcı_Adı);
        Models.Kullanıcı kullanıcı = KullanıcıFonksiyonları.kullanıcıAl_KullanıcıAdı(Kullanıcı_Adı);

        Models.Roller rol_RolVerici = new Models.Roller(RolVerici_KullanıcıK, "Rol Atayıcı/Alıcı");
        bool YetkiVar = RollerFonksiyonları.SatırVar(rol_RolVerici);
        bool OturumAçık = OturumVT.OturumAçık(RolVerici_KullanıcıK, RolVerici_OturumK);

        if(YetkiVar && OturumAçık)
        {
            Models.Roller roller = new Models.Roller(kullanıcı.Kimlik, "Nokta Ekleyici");

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

    [HttpPost("RolVer_NoktaDüzenleyici/{Kullanıcı_Adı}")]
    public IActionResult RolVer_NoktaDüzenleyici(string Kullanıcı_Adı,
            [FromHeader(Name="KULLANICI")] string RolVerici_KullanıcıK, 
            [FromHeader(Name="OTURUM")] string RolVerici_OturumK)
    {
        RolVerici_KullanıcıK = Uri.UnescapeDataString(RolVerici_KullanıcıK);
        RolVerici_OturumK = Uri.UnescapeDataString(RolVerici_OturumK);

        Kullanıcı_Adı = Uri.UnescapeDataString(Kullanıcı_Adı);
        Models.Kullanıcı kullanıcı = KullanıcıFonksiyonları.kullanıcıAl_KullanıcıAdı(Kullanıcı_Adı);

        Models.Roller rol_RolVerici = new Models.Roller(RolVerici_KullanıcıK, "Rol Atayıcı/Alıcı");
        bool YetkiVar = RollerFonksiyonları.SatırVar(rol_RolVerici);
        bool OturumAçık = OturumVT.OturumAçık(RolVerici_KullanıcıK, RolVerici_OturumK);

        if(YetkiVar && OturumAçık)
        {
            Models.Roller roller = new Models.Roller(kullanıcı.Kimlik, "Nokta Düzenleyici");

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

    [HttpPost("RolVer_NoktaSilici/{Kullanıcı_Adı}")]
    public IActionResult RolVer_NoktaSilici(string Kullanıcı_Adı,
            [FromHeader(Name="KULLANICI")] string RolVerici_KullanıcıK, 
            [FromHeader(Name="OTURUM")] string RolVerici_OturumK)
    {
        RolVerici_KullanıcıK = Uri.UnescapeDataString(RolVerici_KullanıcıK);
        RolVerici_OturumK = Uri.UnescapeDataString(RolVerici_OturumK);

        Kullanıcı_Adı = Uri.UnescapeDataString(Kullanıcı_Adı);
        Models.Kullanıcı kullanıcı = KullanıcıFonksiyonları.kullanıcıAl_KullanıcıAdı(Kullanıcı_Adı);

        Models.Roller rol_RolVerici = new Models.Roller(RolVerici_KullanıcıK, "Rol Atayıcı/Alıcı");
        bool YetkiVar = RollerFonksiyonları.SatırVar(rol_RolVerici);
        bool OturumAçık = OturumVT.OturumAçık(RolVerici_KullanıcıK, RolVerici_OturumK);

        if(YetkiVar && OturumAçık)
        {
            Console.WriteLine("yetki var, oturum açık");
            Models.Roller roller = new Models.Roller(kullanıcı.Kimlik, "Nokta Silici");

            if(RollerFonksiyonları.RolVer(roller))
            {
                Console.WriteLine("created");
                return new StatusCodeResult(201); //Created
            }
            else
            {
                Console.WriteLine("unprocessable");
                return new StatusCodeResult(422); //Unprocessable Content
            }
        }
        else
        {
            Console.WriteLine("forbiddden");
            return new StatusCodeResult(403); //Forbidden
        }
    }

    [HttpPost("RolVer_RolAtayıcıAlıcı/{Kullanıcı_Adı}")]
    public IActionResult RolVer_RolAtayıcıAlıcı(string Kullanıcı_Adı,
            [FromHeader(Name="KULLANICI")] string RolVerici_KullanıcıK, 
            [FromHeader(Name="OTURUM")] string RolVerici_OturumK)
    {
        RolVerici_KullanıcıK = Uri.UnescapeDataString(RolVerici_KullanıcıK);
        RolVerici_OturumK = Uri.UnescapeDataString(RolVerici_OturumK);

        Kullanıcı_Adı = Uri.UnescapeDataString(Kullanıcı_Adı);
        Models.Kullanıcı kullanıcı = KullanıcıFonksiyonları.kullanıcıAl_KullanıcıAdı(Kullanıcı_Adı);

        Models.Roller rol_RolVerici = new Models.Roller(RolVerici_KullanıcıK, "Rol Atayıcı/Alıcı");
        bool YetkiVar = RollerFonksiyonları.SatırVar(rol_RolVerici);
        bool OturumAçık = OturumVT.OturumAçık(RolVerici_KullanıcıK, RolVerici_OturumK);

        if(YetkiVar && OturumAçık)
        {
            Models.Roller roller = new Models.Roller(kullanıcı.Kimlik, "Rol Atayıcı/Alıcı");

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

    [HttpPost("RolVer_KullanıcıSilici/{Kullanıcı_Adı}")]
    public IActionResult RolVer_KullanıcıSilici(string Kullanıcı_Adı,
            [FromHeader(Name="KULLANICI")] string RolVerici_KullanıcıK, 
            [FromHeader(Name="OTURUM")] string RolVerici_OturumK)
    {
        RolVerici_KullanıcıK = Uri.UnescapeDataString(RolVerici_KullanıcıK);
        RolVerici_OturumK = Uri.UnescapeDataString(RolVerici_OturumK);

        Kullanıcı_Adı = Uri.UnescapeDataString(Kullanıcı_Adı);
        Models.Kullanıcı kullanıcı = KullanıcıFonksiyonları.kullanıcıAl_KullanıcıAdı(Kullanıcı_Adı);

        Models.Roller rol_RolVerici = new Models.Roller(RolVerici_KullanıcıK, "Rol Atayıcı/Alıcı");
        bool YetkiVar = RollerFonksiyonları.SatırVar(rol_RolVerici);
        bool OturumAçık = OturumVT.OturumAçık(RolVerici_KullanıcıK, RolVerici_OturumK);

        if(YetkiVar && OturumAçık)
        {
            Models.Roller roller = new Models.Roller(kullanıcı.Kimlik, "Kullanıcı Silici");

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

    [HttpDelete("RolAl_NoktaEkleyici/{Kullanıcı_Adı}")]
    public IActionResult RolAl_NoktaEkleyici(string Kullanıcı_Adı, 
            [FromHeader(Name="KULLANICI")] string RolAlıcı_KullanıcıK, 
            [FromHeader(Name="OTURUM")] string RolAlıcı_OturumK)
    {
        RolAlıcı_KullanıcıK = Uri.UnescapeDataString(RolAlıcı_KullanıcıK);
        RolAlıcı_OturumK = Uri.UnescapeDataString(RolAlıcı_OturumK);

        Kullanıcı_Adı = Uri.UnescapeDataString(Kullanıcı_Adı);
        Models.Kullanıcı kullanıcı = KullanıcıFonksiyonları.kullanıcıAl_KullanıcıAdı(Kullanıcı_Adı);
   
        Models.Roller rol_RolAlıcı = new Models.Roller(RolAlıcı_KullanıcıK, "Rol Atayıcı/Alıcı");
        bool YetkiVar = RollerFonksiyonları.SatırVar(rol_RolAlıcı);
        bool OturumAçık = OturumVT.OturumAçık(RolAlıcı_KullanıcıK, RolAlıcı_OturumK);

        if(YetkiVar && OturumAçık)
        {
            Models.Roller roller = new Models.Roller(kullanıcı.Kimlik, "Nokta Ekleyici");

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

    [HttpDelete("RolAl_NoktaDüzenleyici/{Kullanıcı_Adı}")]
    public IActionResult RolAl_NoktaDüzenleyici(string Kullanıcı_Adı, 
            [FromHeader(Name="KULLANICI")] string RolAlıcı_KullanıcıK, 
            [FromHeader(Name="OTURUM")] string RolAlıcı_OturumK)
    {
        RolAlıcı_KullanıcıK = Uri.UnescapeDataString(RolAlıcı_KullanıcıK);
        RolAlıcı_OturumK = Uri.UnescapeDataString(RolAlıcı_OturumK);

        Kullanıcı_Adı = Uri.UnescapeDataString(Kullanıcı_Adı);
        Models.Kullanıcı kullanıcı = KullanıcıFonksiyonları.kullanıcıAl_KullanıcıAdı(Kullanıcı_Adı);

        Models.Roller rol_RolAlıcı = new Models.Roller(RolAlıcı_KullanıcıK, "Rol Atayıcı/Alıcı");
        bool YetkiVar = RollerFonksiyonları.SatırVar(rol_RolAlıcı);
        bool OturumAçık = OturumVT.OturumAçık(RolAlıcı_KullanıcıK, RolAlıcı_OturumK);

        if(YetkiVar && OturumAçık)
        {
            Models.Roller roller = new Models.Roller(kullanıcı.Kimlik, "Nokta Düzenleyici");

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

    [HttpDelete("RolAl_NoktaSilici/{Kullanıcı_Adı}")]
    public IActionResult RolAl_NoktaSilici(string Kullanıcı_Adı,
            [FromHeader(Name="KULLANICI")] string RolAlıcı_KullanıcıK, 
            [FromHeader(Name="OTURUM")] string RolAlıcı_OturumK)
    {
        RolAlıcı_KullanıcıK = Uri.UnescapeDataString(RolAlıcı_KullanıcıK);
        RolAlıcı_OturumK = Uri.UnescapeDataString(RolAlıcı_OturumK);

        Kullanıcı_Adı = Uri.UnescapeDataString(Kullanıcı_Adı);
        Models.Kullanıcı kullanıcı = KullanıcıFonksiyonları.kullanıcıAl_KullanıcıAdı(Kullanıcı_Adı);

        Models.Roller rol_RolAlıcı = new Models.Roller(RolAlıcı_KullanıcıK, "Rol Atayıcı/Alıcı");
        bool YetkiVar = RollerFonksiyonları.SatırVar(rol_RolAlıcı);
        bool OturumAçık = OturumVT.OturumAçık(RolAlıcı_KullanıcıK, RolAlıcı_OturumK);

        if(YetkiVar && OturumAçık)
        {
            Models.Roller roller = new Models.Roller(kullanıcı.Kimlik, "Nokta Silici");

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

    [HttpDelete("RolAl_RolAtayıcıAlıcı/{Kullanıcı_Adı}")]
    public IActionResult RolAl_RolAtayıcıAlıcı(string Kullanıcı_Adı,
            [FromHeader(Name="KULLANICI")] string RolAlıcı_KullanıcıK, 
            [FromHeader(Name="OTURUM")] string RolAlıcı_OturumK)
    {
        RolAlıcı_KullanıcıK = Uri.UnescapeDataString(RolAlıcı_KullanıcıK);
        RolAlıcı_OturumK = Uri.UnescapeDataString(RolAlıcı_OturumK);

        Kullanıcı_Adı = Uri.UnescapeDataString(Kullanıcı_Adı);
        Models.Kullanıcı kullanıcı = KullanıcıFonksiyonları.kullanıcıAl_KullanıcıAdı(Kullanıcı_Adı);

        Models.Roller rol_RolAlıcı = new Models.Roller(RolAlıcı_KullanıcıK, "Rol Atayıcı/Alıcı");
        bool YetkiVar = RollerFonksiyonları.SatırVar(rol_RolAlıcı);
        bool OturumAçık = OturumVT.OturumAçık(RolAlıcı_KullanıcıK, RolAlıcı_OturumK);

        if(YetkiVar && OturumAçık)
        {
            Models.Roller roller = new Models.Roller(kullanıcı.Kimlik, "Rol Atayıcı/Alıcı");

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

    [HttpDelete("RolAl_KullanıcıSilici/{Kullanıcı_Adı}")]
    public IActionResult RolAl_KullanıcıSilici(string Kullanıcı_Adı,
            [FromHeader(Name="KULLANICI")] string RolAlıcı_KullanıcıK,
            [FromHeader(Name="OTURUM")] string RolAlıcı_OturumK)
    {
        RolAlıcı_KullanıcıK = Uri.UnescapeDataString(RolAlıcı_KullanıcıK);
        RolAlıcı_OturumK = Uri.UnescapeDataString(RolAlıcı_OturumK);

        Kullanıcı_Adı = Uri.UnescapeDataString(Kullanıcı_Adı);
        Models.Kullanıcı kullanıcı = KullanıcıFonksiyonları.kullanıcıAl_KullanıcıAdı(Kullanıcı_Adı);

        Models.Roller rol_RolAlıcı = new Models.Roller(RolAlıcı_KullanıcıK, "Rol Atayıcı/Alıcı");
        bool YetkiVar = RollerFonksiyonları.SatırVar(rol_RolAlıcı);
        bool OturumAçık = OturumVT.OturumAçık(RolAlıcı_KullanıcıK, RolAlıcı_OturumK);

        if(YetkiVar && OturumAçık)
        {
            Models.Roller roller = new Models.Roller(kullanıcı.Kimlik, "Kullanıcı Silici");

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

    /**
    * <summary>
    * Kullanıcının kendisinin Kullanıcı Silici rolü olup olmadığını
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
    * Kullanıcının oturumu geçerliyse ve Kullanıcı Silici rolü varsa <c>200 OK</c>,
    * oturum geçersizse veya Kullanıcı Silici rolü yoksa <c>403 Forbidden</c>.
    * </returns>
    */
    [HttpPost("KullanıcıSilebilirim")]
    public IActionResult KullanıcıSilebilirim(
            [FromHeader(Name="KULLANICI")] string KullanıcıKimliği,
            [FromHeader(Name="OTURUM")] string OturumKimliği)
    {
        bool oturum_açık = OturumVT.OturumAçık(KullanıcıKimliği, OturumKimliği);

        if (oturum_açık)
        {
            Models.Kullanıcı kullanıcı = KullanıcıFonksiyonları.kullanıcıAl_Kimlik(KullanıcıKimliği);
            bool rol_var = RollerFonksiyonları.KullanıcıSilebilir(kullanıcı);

            if (rol_var)
            {
                return new StatusCodeResult(200); //OK
            }
        }

        return new StatusCodeResult(403); //Forbidden
    }

    /**
    * <summary>
    * Seçili kullanıcının sahip olduğu rolleri döndürür.
    * </summary>
    * <remarks>
    * <para>
    * HTTP GET türünde bir metoddur. 
    * </para>
    * </remarks>
    *
    * <param name="Kullanıcı_Adı">Rolleri getirilecek kullanıcının adı</param>
    *
    * <returns>
    * Seçili kullanıcının sahip olduğu rolleri
    * </returns>
    */
    [HttpGet("KullanıcınınRolleri")]
    public IActionResult KullanıcınınRolleri(
            [FromHeader(Name="KULLANICI")] string Kullanıcı_Adı)
    {
        try
        {
            List<string> sahipOlunanRoller = [];
            Kullanıcı_Adı = Uri.UnescapeDataString(Kullanıcı_Adı);

            Models.Kullanıcı kullanıcı = 
                KullanıcıFonksiyonları.kullanıcıAl_KullanıcıAdı(Kullanıcı_Adı);

            if(RollerFonksiyonları.NoktaEkleyebilir(kullanıcı))
            {
                sahipOlunanRoller.Add(Rol_M.RollerDizisi[
                    (int)Rol_M.RollerE.NoktaEkleyici]);
            }
            if(RollerFonksiyonları.NoktaDüzenleyebilir(kullanıcı))
            {
                sahipOlunanRoller.Add(Rol_M.RollerDizisi[
                    (int)Rol_M.RollerE.NoktaDüzenleyici]);
            }
            if(RollerFonksiyonları.NoktaSilebilir(kullanıcı))
            {
                sahipOlunanRoller.Add(Rol_M.RollerDizisi[
                    (int)Rol_M.RollerE.NoktaSilici]);
            }
            if(RollerFonksiyonları.RolAtayabilirAlabilir(kullanıcı))
            {
                sahipOlunanRoller.Add(Rol_M.RollerDizisi[
                    (int)Rol_M.RollerE.RolAtayıcıAlıcı]);
            }
            if(RollerFonksiyonları.KullanıcıSilebilir(kullanıcı))
            {
                sahipOlunanRoller.Add(Rol_M.RollerDizisi[
                    (int)Rol_M.RollerE.KullanıcıSilici]);
            }

            string json = JsonConvert.SerializeObject(sahipOlunanRoller);

            JsonResult yanıt = new JsonResult(json);

            if(sahipOlunanRoller.Count() > 0)
            {
                yanıt.StatusCode = 200; //OK
                return yanıt;
            }
            else
            {
                return new StatusCodeResult(204); // No Content
            }
        }
        catch
        {
            StatusCodeResult yanıt = new StatusCodeResult(500); //Internal Server Error
            return yanıt;
        }

    }

    /**
    * <summary>
    * Seçili kullanıcının sahip olmadığı rolleri döndürür.
    * </summary>
    * <remarks>
    * <para>
    * HTTP GET türünde bir metoddur. 
    * </para>
    * </remarks>
    *
    * <param name="Kullanıcı_Adı">Sahip olmadığı rolleri getirilecek 
    * kullanıcının adı</param>
    *
    * <returns>
    * Seçili kullanıcının sahip olmadığı rolleri
    * </returns>
    */
    [HttpGet("KullanıcınınRolleriDeğil")]
    public IActionResult KullanıcınınRolleriDeğil(
            [FromHeader(Name="KULLANICI")] string Kullanıcı_Adı)
    {
        try
        {
            List<string> sahipOlunmayanRoller = [];
            Kullanıcı_Adı = Uri.UnescapeDataString(Kullanıcı_Adı);

            Models.Kullanıcı kullanıcı = 
                KullanıcıFonksiyonları.kullanıcıAl_KullanıcıAdı(Kullanıcı_Adı);

            if(!RollerFonksiyonları.NoktaEkleyebilir(kullanıcı))
            {
                sahipOlunmayanRoller.Add(Rol_M.RollerDizisi[
                    (int)Rol_M.RollerE.NoktaEkleyici]);
            }
            if(!RollerFonksiyonları.NoktaDüzenleyebilir(kullanıcı))
            {
                sahipOlunmayanRoller.Add(Rol_M.RollerDizisi[
                    (int)Rol_M.RollerE.NoktaDüzenleyici]);
            }
            if(!RollerFonksiyonları.NoktaSilebilir(kullanıcı))
            {
                sahipOlunmayanRoller.Add(Rol_M.RollerDizisi[
                    (int)Rol_M.RollerE.NoktaSilici]);
            }
            if(!RollerFonksiyonları.RolAtayabilirAlabilir(kullanıcı))
            {
                sahipOlunmayanRoller.Add(Rol_M.RollerDizisi[
                    (int)Rol_M.RollerE.RolAtayıcıAlıcı]);
            }
            if(!RollerFonksiyonları.KullanıcıSilebilir(kullanıcı))
            {
                sahipOlunmayanRoller.Add(Rol_M.RollerDizisi[
                    (int)Rol_M.RollerE.KullanıcıSilici]);
            }

            string json = JsonConvert.SerializeObject(sahipOlunmayanRoller);

            JsonResult yanıt = new JsonResult(json);

            if(sahipOlunmayanRoller.Count() > 0)
            {
                yanıt.StatusCode = 200; //OK
                return yanıt;
            }
            else
            {
                return new StatusCodeResult(204); // No Content
            }
        }
        catch
        {
            StatusCodeResult yanıt = new StatusCodeResult(500); //Internal Server Error
            return yanıt;
        }

    }
}