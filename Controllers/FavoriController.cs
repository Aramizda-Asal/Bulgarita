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

namespace bulgarita.Controllers;

[ApiController]
[Route("[controller]")]

public class Favori : ControllerBase
{
    [HttpPost("FavoriEkle")]
    public IActionResult FavoriEkle(
            [FromHeader(Name="KULLANICI")] string Kullanıcı_Kimliği,
            [FromHeader(Name="OTURUM")] string Oturum_Kimliği,
            [FromBody] string body)
    {
        if(OturumVT.OturumAçık(Kullanıcı_Kimliği, Oturum_Kimliği))
        {
            if(FavorilerFonksiyonları.FavoriEkle(Kullanıcı_Kimliği, body))
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

    [HttpDelete("FavorilerdenCikar")]
    public IActionResult FavorilerdenCikar(
            [FromHeader(Name="KULLANICI")] string Kullanıcı_Kimliği,
            [FromHeader(Name="OTURUM")] string Oturum_Kimliği,
            [FromBody] string body)
    {
        if(OturumVT.OturumAçık(Kullanıcı_Kimliği, Oturum_Kimliği))
        {
            if(FavorilerFonksiyonları.FavorilerdenCikar(Kullanıcı_Kimliği, body))
            {
                return new StatusCodeResult(200); //Ok
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

    [HttpGet("FavorileriGöster")]
    public IActionResult FavorileriAl(
            [FromHeader(Name ="KULLANICI")] string Kullanıcı_Kimliği, 
            [FromHeader(Name="OTURUM")] string Oturum_Kimliği)
    {
        if(OturumVT.OturumAçık(Kullanıcı_Kimliği, Oturum_Kimliği))
        {
            List<String> Favori_Kimlikleri = FavorilerFonksiyonları.FavorileriAl(Kullanıcı_Kimliği);

            if(Favori_Kimlikleri != null)
            {
                JsonResult yanıt = new JsonResult(Newtonsoft.Json.JsonConvert.SerializeObject(Favori_Kimlikleri));
                yanıt.StatusCode = 200; // OK
                return yanıt;
            }
            else
            {
                return new StatusCodeResult(204); //No Content
            }
        }
        else
        {
            return new StatusCodeResult(403); //Forbidden
        }
    }

    [HttpPost("SatirVarMi")]
    public IActionResult SatirVarMi(
            [FromHeader(Name="KULLANICI")] string Kullanıcı_Kimliği,
            [FromHeader(Name="OTURUM")] string Oturum_Kimliği,
            [FromBody] string body)
    {
        if(OturumVT.OturumAçık(Kullanıcı_Kimliği, Oturum_Kimliği))
        {
            if(FavorilerFonksiyonları.SatırVar(Kullanıcı_Kimliği, body))
            {
                return new StatusCodeResult(200); //OK
            }
            else
            {
                return new StatusCodeResult(404); //Not Found
            }
        }
        else
        {
            return new StatusCodeResult(403); //Forbidden
        }
    }
}
    