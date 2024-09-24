using Microsoft.AspNetCore.Mvc;
using bulgarita.Services;
using bulgarita.Models;
using bulgarita;
using Microsoft.AspNetCore.Http.HttpResults;

namespace bulgarita.Controllers;

[ApiController]
[Route("[controller]")]

public class Harita : ControllerBase
{
    [HttpGet("NoktaAl/{bölge_türü}")]
    public string NoktaAl(string bölge_türü)
    {
        List<Harita> BölgeListe = HaritaFonksiyonları.BölgelerinBilgileriniAl(bölge_türü);
        return "ok";
    }
}