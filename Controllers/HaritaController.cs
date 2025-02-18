using Microsoft.AspNetCore.Mvc;
using bulgarita.Services;
using bulgarita.Models;
using bulgarita;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;

namespace bulgarita.Controllers;

[ApiController]
[Route("[controller]")]

public class Harita : ControllerBase
{
    [HttpGet("NoktaAl/{bölge_türü}")]
    public string[][] NoktaAl(string bölge_türü)
    {
        List<Models.Harita> BölgeListe = HaritaFonksiyonları.BölgelerinBilgileriniAl(bölge_türü);
        string[][] BölgeListeDizi = new string[BölgeListe.Count()][]; //ikinci boyutun boyutu hep 9.
        int index = 0;
        foreach (Models.Harita nokta in BölgeListe)
        {
            BölgeListeDizi[index] = nokta.ToStringArray();
            index++;
        }
        return BölgeListeDizi;
    }

    [HttpGet("NoktaAl")]
    public string[][] NoktaAl()
    {
        List<Models.Harita> BölgeListe = HaritaFonksiyonları.BölgelerinBilgileriniAl();
        string[][] BölgeListeDizi = new string[BölgeListe.Count()][]; //ikinci boyutun boyutu hep 9.
        int index = 0;
        foreach (Models.Harita nokta in BölgeListe)
        {
            BölgeListeDizi[index] = nokta.ToStringArray();
            index++;
        }
        return BölgeListeDizi;
    }

    [HttpPost(
    "NoktaKoy/{EnlemDrc}/{BoylamDrc}/{Bulgarca_Latin_İsim}/{Bulgarca_Kiril_İsim}/{Türkçe_İsim}/{Osmanlıca_İsim}/{Bölge_Türü}/{Üst_Bölge}/{Kimlik}/{Ekleyici_KullanıcıK}/{Ekleyici_OturumK}")]
    public IActionResult NoktaKoy(double EnlemDrc, double BoylamDrc, string Bulgarca_Latin_İsim, string Bulgarca_Kiril_İsim, string Türkçe_İsim,
                                  string Osmanlıca_İsim, string Bölge_Türü, string Üst_Bölge, string Kimlik,
                                  string Ekleyici_KullanıcıK, string Ekleyici_OturumK)
    {
        Kimlik = Uri.UnescapeDataString(Kimlik);
        Üst_Bölge = Uri.UnescapeDataString(Üst_Bölge);
        Ekleyici_KullanıcıK = Uri.UnescapeDataString(Ekleyici_KullanıcıK);
        Ekleyici_OturumK = Uri.UnescapeDataString(Ekleyici_OturumK);

        Models.Roller rol_Ekleyici = new Models.Roller(Ekleyici_KullanıcıK, "Nokta Ekleyici");
        bool YetkiVar = RollerFonksiyonları.SatırVar(rol_Ekleyici);
        bool OturumAçık = OturumVT.OturumAçık(Ekleyici_KullanıcıK, Ekleyici_OturumK);

        if(YetkiVar && OturumAçık)
        {
            if(HaritaFonksiyonları.BölgeninBilgileriniKoy(EnlemDrc, BoylamDrc, Bulgarca_Latin_İsim, Bulgarca_Kiril_İsim, Türkçe_İsim, Osmanlıca_İsim, Bölge_Türü, Üst_Bölge, Kimlik))
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

    [HttpPatch("NoktaBilgisiGüncelle/{kimlik}/{veri_sütunu}/{yeni_veri}/{Düzenleyici_KullanıcıK}/{Düzenleyici_OturumK}")]
    public IActionResult NoktaBilgisiGüncelle(string kimlik, string veri_sütunu, string yeni_veri, string Düzenleyici_KullanıcıK, string Düzenleyici_OturumK)
    {
        kimlik = Uri.UnescapeDataString(kimlik);
        yeni_veri = Uri.UnescapeDataString(yeni_veri);
        Düzenleyici_KullanıcıK = Uri.UnescapeDataString(Düzenleyici_KullanıcıK);
        Düzenleyici_OturumK = Uri.UnescapeDataString(Düzenleyici_OturumK);

        Models.Roller rol_Düzenleyici = new Models.Roller(Düzenleyici_KullanıcıK, "Nokta Düzenleyici");
        bool YetkiVar = RollerFonksiyonları.SatırVar(rol_Düzenleyici);
        bool OturumAçık = OturumVT.OturumAçık(Düzenleyici_KullanıcıK, Düzenleyici_OturumK);

        if(YetkiVar && OturumAçık)
        {
            if(HaritaFonksiyonları.BölgeBilgileriniDeğis(kimlik, veri_sütunu, yeni_veri))
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

    [HttpDelete("NoktaSil/{Kimlik}/{Silici_KullanıcıK}/{Silici_OturumK}")]
    public IActionResult NoktaSil(string Kimlik, string Silici_KullanıcıK, string Silici_OturumK)
    {
        Kimlik = Uri.UnescapeDataString(Kimlik);
        Silici_KullanıcıK = Uri.UnescapeDataString(Silici_KullanıcıK);
        Silici_OturumK = Uri.UnescapeDataString(Silici_OturumK);

        Models.Roller rol_Silici = new Models.Roller(Silici_KullanıcıK, "Nokta Silici");
        bool YetkiVar = RollerFonksiyonları.SatırVar(rol_Silici);
        bool OturumAçık = OturumVT.OturumAçık(Silici_KullanıcıK, Silici_OturumK);

        if(YetkiVar && OturumAçık)
        {
           if(HaritaFonksiyonları.BölgeSil(Kimlik))
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