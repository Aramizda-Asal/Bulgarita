using Microsoft.AspNetCore.Mvc;
using bulgarita.Services;
using bulgarita.Models;
using bulgarita;
using System.Collections.Specialized;
using Newtonsoft.Json;
using System.Text.Json.Nodes;

namespace bulgarita.Controllers;

[ApiController]
[Route("[controller]")]

public class Kullanıcı: ControllerBase
{
    [HttpPost("KullanıcıEkle/{Kullanıcı_Adı}/{E_Posta}/{Parola}")]
    public IActionResult KullanıcıEkle(string Kullanıcı_Adı, string E_Posta, string Parola)
    {
        string Kimlik = KullanıcıFonksiyonları.Yeni_Kimlik();
        Models.Kullanıcı kullanıcı = new Models.Kullanıcı(
                Uri.UnescapeDataString(Kullanıcı_Adı),
                Uri.UnescapeDataString(E_Posta),
                Uri.UnescapeDataString(Parola),
                Kimlik );

        if(KullanıcıFonksiyonları.kullanıcıEkle(kullanıcı))
        {
            return new StatusCodeResult(201); //Created
        }
        else
        {
            return new StatusCodeResult(422); //Unprocessable Content
        }
    }

    /**
    * <summary>
    * Kullanıcıların kendi kullanıcı adlarını değiştirebilmeleri için API denetçisi.
    * </summary>
    * <remarks>
    * <para>
    * HTTP PATCH türünde bir metoddur. Yalnızca durum kodu döndürür.
    * </para>
    * <para>
    * <see cref="bulgarita.Services.KullanıcıFonksiyonları.KullanıcıAdıDeğiştir(string, string, string)">
    * Kullanıcı kimliği ile parola eşleşiyorsa kullanıcı adını
    * güncelleyen hizmeti</see> kullanır.
    * </para>
    * </remarks>
    *
    * <param name="Kullanıcı">Kullanıcı adını değiştirecek kullanıcının kimliği</param>
    * <param name="Parola">Kimlik doğrulama amacıyla kullanıcının karılmamış parolası</param>
    * <param name="YeniKullanıcıAdı">Kullanıcının yeni belirlediği kullanıcı adı</param>
    *
    * <seealso cref="bulgarita.Services.KullanıcıFonksiyonları.KullanıcıAdıDeğiştir(string, string, string)"/>
    */
    [HttpPatch("KullanıcıAdıDeğiştir/{Kullanıcı}/{Parola}/{YeniKullanıcıAdı}/")]
    public IActionResult KullanıcıAdıDeğiştir(string Kullanıcı, string Parola,
                                              string YeniKullanıcıAdı)
    {
        Kullanıcı = Uri.UnescapeDataString(Kullanıcı);
        Parola = Uri.UnescapeDataString(Parola);
        YeniKullanıcıAdı = Uri.UnescapeDataString(YeniKullanıcıAdı);

        if(KullanıcıFonksiyonları.KullanıcıAdıDeğiştir(Kullanıcı, Parola, YeniKullanıcıAdı))
        {
            return new StatusCodeResult(200); //OK
        }
        else
        {
            return new StatusCodeResult(403); //Forbidden
        }
    }

    /**
    * <summary>
    * Kullanıcıların kendi parolalarını değiştirebilmesi için API denetçisi.
    * </summary>
    * <remarks>
    * <para>
    * HTTP PATCH türünde bir metoddur. Yalnızca durum kodu döndürür.
    * </para>
    * <para>
    * Kullanıcı kimliği ile şu anki parola eşleşiyorsa
    * <see cref="bulgarita.Services.KullanıcıFonksiyonları.ParolaDeğiştir(string, string, string)">
    * yeni parolayı karıp veri tabanına kaydeden hizmeti
    * </see>
    * kullanır.
    * </para>
    * </remarks>
    *
    * <param name="Kullanıcı">Parolasının değiştirecek kullanıcının kimliği</param>
    * <param name="MevcutParola">Kullanıcının şu anki parolasının karılmamış hâli</param>
    * <param name="YeniParola">Kullanıcının yeni belirlediği parola (karılmamış)</param>
    *
    * <returns>
    * Parola başarıyla değiştirilirse <c>200 OK</c>,
    * değiştirilmezse <c>403 Forbidden</c>.
    * </returns>
    *
    * <seealso cref="bulgarita.Services.KullanıcıFonksiyonları.ParolaDeğiştir(string, string, string)"/>
    */
    [HttpPatch("ParolaDeğiştir/{Kullanıcı}/{MevcutParola}/{YeniParola}/")]
    public IActionResult ParolaDeğiştir(string Kullanıcı, string MevcutParola,
                                        string YeniParola)
    {
        Kullanıcı = Uri.UnescapeDataString(Kullanıcı);
        MevcutParola = Uri.UnescapeDataString(MevcutParola);
        YeniParola = Uri.UnescapeDataString(YeniParola);

        if(KullanıcıFonksiyonları.ParolaDeğiştir(Kullanıcı, MevcutParola, YeniParola))
        {
            return new StatusCodeResult(200); //OK
        }
        else
        {
            return new StatusCodeResult(403); //Forbidden
        }
    }

    [HttpDelete("KullanıcıSil")]
    public IActionResult KullanıcıSil(
            [FromHeader(Name="KULLANICI")] string Kullanıcı_Kimliği,
            [FromHeader(Name="OTURUM")] string Oturum_Kimliği,
            [FromBody] string body)
    {
        Models.Kullanıcı silinecek_kullanıcı = KullanıcıFonksiyonları.kullanıcıAl_KullanıcıAdı(body);
        Models.Roller rol_KullanıcıSilici = new Models.Roller(Kullanıcı_Kimliği, "Kullanıcı Silici");
        bool YetkiVar = RollerFonksiyonları.SatırVar(rol_KullanıcıSilici);
        bool OturumAçık = OturumVT.OturumAçık(Kullanıcı_Kimliği, Oturum_Kimliği);

        if(YetkiVar && OturumAçık)
        {
            if(KullanıcıFonksiyonları.KullanıcıSil(silinecek_kullanıcı.Kimlik))
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

    [HttpGet("KullanıcıAra/{içerik}")]
    public IActionResult KullanıcıAra(string içerik)
    {
        try
        {
            List<string> sonuç = KullanıcıFonksiyonları.İçerenKullanıcıAdlarıAl(içerik);

            if(sonuç.Count < 1)
            {
                StatusCodeResult yanıt = new StatusCodeResult(204); //No Content
            }

            string json = JsonConvert.SerializeObject(sonuç);

            JsonResult JSON_yanıt = new JsonResult(json);
            JSON_yanıt.StatusCode = 200; //OK
            return JSON_yanıt;
        }
        catch
        {
            StatusCodeResult yanıt = new StatusCodeResult(500); //Internal Server Error
            return yanıt;
        }
    }
}