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

using bulgarita.Models;
using bulgarita;
using MySql.Data.MySqlClient;

namespace bulgarita.Services;

public static class RollerFonksiyonları
{
    public static bool RolVer(Models.Roller roller)
    {
        string cs = Bağlantı.bağlantı_dizisi;
        
        MySqlConnection bağlantı = new MySqlConnection(cs);
        bağlantı.Open();

        if(!SatırVar_AçıkBağlantı(roller, bağlantı))
        {
            if(KullanıcıFonksiyonları.VeriVarAçık("Kimlik", roller.Kullanıcı, bağlantı))
            {
                string kod = $"INSERT INTO {Bağlantı.Roller_Tablosu} VALUES(@kullanıcı_kimliği, @rol);";

                MySqlCommand komut = new MySqlCommand(kod, bağlantı);

                komut.Parameters.AddWithValue("@kullanıcı_kimliği", roller.Kullanıcı);
                komut.Parameters.AddWithValue("@rol", roller.Rol);

                komut.ExecuteNonQuery();
                komut.Dispose();

                bağlantı.Close();
                bağlantı.Dispose();

                return true;
            }
        }
        return false;
    } 

    public static bool RolAl(Models.Roller roller)
    {
        string cs = Bağlantı.bağlantı_dizisi;
        
        MySqlConnection bağlantı = new MySqlConnection(cs);
        bağlantı.Open();

        if(SatırVar_AçıkBağlantı(roller, bağlantı))
        {
            string kod = $"DELETE FROM {Bağlantı.Roller_Tablosu} ";
            kod += $"WHERE Kullanıcı = @kullanıcı_kimliği AND Rol = @rol;";

            MySqlCommand komut = new MySqlCommand(kod, bağlantı);

            komut.Parameters.AddWithValue("@kullanıcı_kimliği", roller.Kullanıcı);
            komut.Parameters.AddWithValue("@rol", roller.Rol);

            komut.ExecuteNonQuery();
            komut.Dispose();

            bağlantı.Close();
            bağlantı.Dispose();

            return true;
        }
        return false;
    }

    /**
    * <summary>
    * Belirtilen kullanıcının nokta ekleme yetkisi olup olmadığını denetler.
    * </summary>
    *
    * <param name="kullanıcı">Rolleri incelenen kullanıcının nesnesi</param>
    *
    * <returns>
    * Kullanıcının Nokta Ekleyici rolü varsa <c>true</c>,
    * yoksa <c>false</c>.
    * </returns>
    */
    public static bool NoktaEkleyebilir(Kullanıcı kullanıcı)
    {
        Roller aranan = new Roller(kullanıcı.Kimlik, "Nokta Ekleyici");
        return SatırVar(aranan);
    }

    /**
    * <summary>
    * Belirtilen kullanıcının nokta düzenleme yetkisi olup olmadığını denetler.
    * </summary>
    *
    * <param name="kullanıcı">Rolleri incelenen kullanıcının nesnesi</param>
    *
    * <returns>
    * Kullanıcının Nokta Düzenleyici rolü varsa <c>true</c>,
    * yoksa <c>false</c>.
    * </returns>
    */
    public static bool NoktaDüzenleyebilir(Kullanıcı kullanıcı)
    {
        Roller aranan = new Roller(kullanıcı.Kimlik, "Nokta Düzenleyici");
        return SatırVar(aranan);
    }

    public static List<Models.Kullanıcı> NoktaDüzenleyiciler()
    {
        string cs = Bağlantı.bağlantı_dizisi;

        MySqlConnection bağlantı = new MySqlConnection(cs);
        bağlantı.Open();

        string kod = $"SELECT k.* FROM {Bağlantı.Kullanıcı_Tablosu} k " +
            $"INNER JOIN {Bağlantı.Roller_Tablosu} r ON r.Kullanıcı = k.Kimlik " +
            "WHERE Rol = \"Nokta Düzenleyici\"";

        MySqlCommand komut = new MySqlCommand(kod, bağlantı);
        MySqlDataReader okuyucu = komut.ExecuteReader();

        List<Models.Kullanıcı> sonuç = new List<Models.Kullanıcı>();
        while (okuyucu.Read())
        {
            Kullanıcı yeni = new Kullanıcı(
                okuyucu["Kullanıcı_Adı"].ToString(),
                okuyucu["E_Posta"].ToString(),
                okuyucu["Parola"].ToString(),
                okuyucu["Kimlik"].ToString()
            );
            sonuç.Add(yeni);
        }
        okuyucu.Close();
        komut.Dispose();
        bağlantı.Close();
        bağlantı.Dispose();

        return sonuç;
    }

    /**
    * <summary>
    * Belirtilen kullanıcının nokta silme yetkisi olup olmadığını denetler.
    * </summary>
    *
    * <param name="kullanıcı">Rolleri incelenen kullanıcının nesnesi</param>
    *
    * <returns>
    * Kullanıcının Nokta Silici rolü varsa <c>true</c>,
    * yoksa <c>false</c>.
    * </returns>
    */
    public static bool NoktaSilebilir(Kullanıcı kullanıcı)
    {
        Roller aranan = new Roller(kullanıcı.Kimlik, "Nokta Silici");
        return SatırVar(aranan);
    }

    /**
    * <summary>
    * Belirtilen kullanıcının rol atama/alma yetkisi olup olmadığını denetler.
    * </summary>
    *
    * <param name="kullanıcı">Rolleri incelenen kullanıcının nesnesi</param>
    *
    * <returns>
    * Kullanıcının Rol Atayıcı/Alıcı rolü varsa <c>true</c>,
    * yoksa <c>false</c>.
    * </returns>
    */
    public static bool RolAtayabilirAlabilir(Kullanıcı kullanıcı)
    {
        Roller aranan = new Roller(kullanıcı.Kimlik, "Rol Atayıcı/Alıcı");
        return SatırVar(aranan);
    }

    /**
    * <summary>
    * Belirtilen kullanıcının kullanıcı silme yetkisi olup olmadığını denetler.
    * </summary>
    *
    * <param name="kullanıcı">Rolleri incelenen kullanıcının nesnesi</param>
    *
    * <returns>
    * Kullanıcının Kullanıcı Silici rolü varsa <c>true</c>,
    * yoksa <c>false</c>.
    * </returns>
    */
    public static bool KullanıcıSilebilir(Kullanıcı kullanıcı)
    {
        Roller aranan = new Roller(kullanıcı.Kimlik, "Kullanıcı Silici");
        return SatırVar(aranan);
    }

    public static bool SatırVar_AçıkBağlantı(Models.Roller roller, MySqlConnection Açık_Bağlantı)
    {
        string kod = $"SELECT COUNT(Rol) FROM {Bağlantı.Roller_Tablosu} ";
        kod += $"WHERE Kullanıcı = @kullanıcı_kimliği AND Rol = @rol";

        MySqlCommand komut = new MySqlCommand(kod, Açık_Bağlantı);

        komut.Parameters.AddWithValue("@kullanıcı_kimliği", roller.Kullanıcı);
        komut.Parameters.AddWithValue("@rol", roller.Rol);

        int sonuc = int.Parse(komut.ExecuteScalar().ToString());

        komut.Dispose();
        return sonuc >= 1;
    }

    public static bool SatırVar(Models.Roller roller)
    {
        string cs = Bağlantı.bağlantı_dizisi;
        
        MySqlConnection bağlantı = new MySqlConnection(cs);
        bağlantı.Open();

        string kod = $"SELECT COUNT(Rol) FROM {Bağlantı.Roller_Tablosu} ";
        kod += $"WHERE Kullanıcı = @kullanıcı_kimliği AND Rol = @rol";

        MySqlCommand komut = new MySqlCommand(kod, bağlantı);

        komut.Parameters.AddWithValue("@kullanıcı_kimliği", roller.Kullanıcı);
        komut.Parameters.AddWithValue("@rol", roller.Rol);

        int sonuc = int.Parse(komut.ExecuteScalar().ToString());

        komut.Dispose();

        bağlantı.Close();
        bağlantı.Dispose();

        return sonuc >= 1;
    }
}