/*
 * Copyright (C) 2025 Güneş Balcı, Habil Tataroğulları, Yusuf Kozan
 *
 * This file is part of "Bulgaristan’da Harita Bazlı Yer İsimleri Uygulaması".
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Affero General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Affero General Public License for more details.
 * 
 * You should have received a copy of the GNU Affero General Public License
 * along with this program.  If not, see <https://www.gnu.org/licenses/>.
 * 
 */

using Microsoft.AspNetCore.Mvc;
using bulgarita.Services;
using Newtonsoft.Json;
using bulgarita;
using System.Net;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;
using System.Text;

namespace bulgarita.Controllers;

[ApiController]
[Route("[controller]")]

public class Harita : ControllerBase
{
    [HttpGet("GeçerliNoktaTürleri")]
    public IActionResult GeçerliNoktaTürleri()
    {
        try 
        {
            int tür_niceliği = 0;
            for (int a = 0; a < Models.Harita.UygunTürler.Length; a++)
            {
                tür_niceliği += Models.Harita.UygunTürler[a].Length;
            }

            string[] sonuç = new string[tür_niceliği];
            int i = 0;
            for (int a = 0; a < Models.Harita.UygunTürler.Length; a++)
            {
                for (int b = 0; b < Models.Harita.UygunTürler[a].Length; b++)
                {
                    sonuç[i] = Models.Harita.UygunTürler[a][b];
                    i++;
                }
            }

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

    [HttpGet("ÜsteGelebilecekNoktalar/{BölgeTürü}/")]
    public IActionResult ÜsteGelebilecekNoktalar(string BölgeTürü)
    {
        BölgeTürü = Uri.UnescapeDataString(BölgeTürü);

        int düzey = HaritaFonksiyonları.BölgeTürüDüzeyi(BölgeTürü);
        if (düzey == -1)
        {
            return new StatusCodeResult(404); // Not Found
        }
        else if (düzey == 0)
        {
            return new StatusCodeResult(204); // No Content
        }

        düzey--; // Bir üst düzeye erişmek için indis eksiltiliyor.
        List<Models.Harita> noktalar = new List<Models.Harita>();
        for (int a = 0; a < Models.Harita.UygunTürler[düzey].Length; a++)
        {
            noktalar.AddRange(
                HaritaFonksiyonları.BölgelerinBilgileriniAl(
                    Models.Harita.UygunTürler[düzey][a]
                )
            );
        }

        if (noktalar.Count < 1)
        {
            return new StatusCodeResult(204); // No Content
        }

        string json = JsonConvert.SerializeObject(noktalar);
        JsonResult yanıt = new JsonResult(json);
        yanıt.StatusCode = 200; // OK
        return yanıt;
    }

    [HttpPut("NoktaEkle/")]
    public IActionResult NoktaEkle(
            [FromHeader(Name="KULLANICI")] string KullanıcıKimliği,
            [FromHeader(Name="OTURUM")] string OturumKimliği,
            [FromBody] JsonObject gövde)
    {
        bool oturum_açık = OturumVT.OturumAçık(KullanıcıKimliği, OturumKimliği);

        if (oturum_açık)
        {
            if (Request.ContentType != "application/json")
            {
                return new StatusCodeResult(400); // Bad Request
            }
            
            try
            {
                Models.Harita gelen = JsonConvert.DeserializeObject<Models.Harita>(gövde.ToString());
                Models.Harita yeni = Models.Harita.YeniNokta(
                    gelen.EnlemDrc,
                    gelen.BoylamDrc,
                    gelen.Bulgarca_Latin_İsim,
                    gelen.Bulgarca_Kiril_İsim,
                    gelen.Türkçe_İsim,
                    gelen.Osmanlıca_İsim,
                    gelen.Bölge_Türü,
                    gelen.Üst_Bölge,
                    gelen.Aciklama
                );
                
                if (yeni != null)
                {
                    if (HaritaFonksiyonları.YeniBölgeKaydet(yeni))
                    {
                        return new StatusCodeResult(200); // OK
                    }
                }

                return new StatusCodeResult(500); // Internal Server Error
            }
            catch
            {
                return new StatusCodeResult(500); // Internal Server Error
            }
        }
        else
        {
            return new StatusCodeResult(403); // Forbidden
        }
    }

    [HttpPut("NoktaGüncelle")]
    public IActionResult NoktaGüncelle(
            [FromHeader(Name="KULLANICI")] string KullanıcıKimliği,
            [FromHeader(Name="OTURUM")] string OturumKimliği,
            [FromBody] JsonObject gövde)
    {
        Models.Harita nokta = JsonConvert.DeserializeObject<Models.Harita>(gövde.ToString());
        if (String.IsNullOrWhiteSpace(nokta.Üst_Bölge))
        {
            nokta.Üst_Bölge = null;
        }
        if (OturumVT.OturumAçık(KullanıcıKimliği, OturumKimliği))
        {
            if (Request.ContentType != "application/json")
            {
                return new StatusCodeResult(400); // Bad Request
            }
            if(HaritaFonksiyonları.BölgeBilgileriniGüncelle(nokta))
            {
                return new StatusCodeResult(200); // OK
            }
            else
            {
                return new StatusCodeResult(500); // Internal Server Error
            }
            
        }
        else
        {
            return new StatusCodeResult(403); // Forbidden
        }
        
    }

    [HttpDelete("NoktaSil")]
    public IActionResult NoktaSil(
        [FromHeader(Name="KULLANICI")] string KullanıcıKimliği,
        [FromHeader(Name="OTURUM")] string OturumKimliği,
        [FromHeader(Name ="NOKTA")] string NoktaKimliği)
    {

        Models.Roller rol_Silici = new Models.Roller(KullanıcıKimliği, "Nokta Silici");
        bool YetkiVar = RollerFonksiyonları.SatırVar(rol_Silici);
        bool OturumAçık = OturumVT.OturumAçık(KullanıcıKimliği, OturumKimliği);

        if(YetkiVar && OturumAçık)
        {
           if(HaritaFonksiyonları.BölgeSil(NoktaKimliği))
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

    [HttpPost("NoktaGeriBildirimi/")]
    public IActionResult NoktaGeriBildirimi(
        [FromHeader(Name="KULLANICI")] string KullanıcıKimliği,
        [FromHeader(Name="OTURUM")] string OturumKimliği,
        [FromBody] JsonObject token
    )
    {
        bool OturumAçık = OturumVT.OturumAçık(KullanıcıKimliği, OturumKimliği);

        if(OturumAçık)
        {
            string nokta_kimliği = token["Nokta"]?.ToString();
            string geri_bildirim = token["Yorum"]?.ToString();

            Models.Harita nokta = HaritaFonksiyonları.BölgeninBilgileriniAl(nokta_kimliği);

            Models.Kullanıcı bildiren = KullanıcıFonksiyonları.kullanıcıAl_Kimlik(KullanıcıKimliği);
            List<Models.Kullanıcı> alıcılar = RollerFonksiyonları.NoktaDüzenleyiciler();

            StringBuilder içerik = new StringBuilder();
            içerik.Append("Haritadaki bir nokta hakkında geri bildirim var.\n");
            içerik.Append("Nokta Bilgileri:\n");
            içerik.Append($"\tBölge Türü: {nokta.Bölge_Türü}\n");
            içerik.Append($"\tBulgarca Latin: {nokta.Bulgarca_Latin_İsim}\n");
            içerik.Append($"\tBulgarca Kiril: {nokta.Bulgarca_Kiril_İsim}\n");
            içerik.Append($"\tTürkçe: {nokta.Türkçe_İsim}\n");
            içerik.Append($"\tOsmanlıca: {nokta.Osmanlıca_İsim}\n");
            içerik.Append($"\tKoordinatlar: {nokta.EnlemDrc}, {nokta.BoylamDrc}\n");
            içerik.Append($"\tAçıklama: {nokta.Aciklama}\n");
            içerik.Append($"\tKimlik: {nokta.Kimlik}\n");
            içerik.Append("\n-----\n\n");
            içerik.Append($"Geri Bildirim Yapan: {bildiren.Adı}\n");
            içerik.Append($"E-Posta Adresi: {bildiren.E_posta}\n");
            içerik.Append($"Bildirim Tarihi: {DateTime.UtcNow} UTC\n");
            içerik.Append("\n-----\n\n");
            içerik.Append("İleti:\n");
            içerik.Append(geri_bildirim);

            Console.WriteLine(içerik.ToString());

            bool gönderildi = PostaFonksiyonları.EPostaGönder(
                "Bulgaristan Yer Adları Geri Bildirimi",
                içerik.ToString(),
                alıcılar
            );

            if (gönderildi)
                return new StatusCodeResult(200);
            else
                return new StatusCodeResult(500);
        }
        return new StatusCodeResult(403);
    }
}