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
    [HttpPost("NoktaKoy/{EnlemDrc}/{BoylamDrc}/{Bulgarca_Latin_İsim}/{Bulgarca_Kiril_İsim}/{Türkçe_İsim}/{Osmanlıca_İsim}/{Bölge_Türü}/{Üst_Bölge}/{Kimlik}")]
    public IActionResult NoktaKoy(double EnlemDrc, double BoylamDrc, string Bulgarca_Latin_İsim, string Bulgarca_Kiril_İsim, string Türkçe_İsim,
                                  string Osmanlıca_İsim, string Bölge_Türü, string Üst_Bölge, string Kimlik)
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

    [HttpPatch("NoktaBilgisiGüncelle/{kimlik}/{veri_sütunu}/{yeni_veri}")]
    public IActionResult NoktaBilgisiGüncelle(string kimlik, string veri_sütunu, string yeni_veri)
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
}