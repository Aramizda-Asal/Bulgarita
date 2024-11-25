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
    [HttpPost("NoktaKoy/{bölge_bilgileri}")]
    public IActionResult NoktaKoy(string bölge_bilgileri)
    {
        bölge_bilgileri = WebUtility.UrlDecode(bölge_bilgileri);
        Models.Harita bölge_bilgileriHarita = new Models.Harita(bölge_bilgileri);
        if(HaritaFonksiyonları.BölgelerinBilgileriniKoy(bölge_bilgileriHarita) == true)
        {
            return new StatusCodeResult(200);
        }
        else
        {
            return new StatusCodeResult(403);
        }
    }
}