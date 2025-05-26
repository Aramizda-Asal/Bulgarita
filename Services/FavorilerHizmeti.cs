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
using System.Text;
using System.Security.Cryptography;


namespace bulgarita.Services;

public static class FavorilerFonksiyonları
{
    public static void VeriGuncelle(string kimlik, string veri_sütunu, string yeni_veri)
    {
        string cs = Bağlantı.bağlantı_dizisi;
        
        MySqlConnection bağlantı = new MySqlConnection(cs);
        bağlantı.Open();

        string kod = $"Update {Bağlantı.Favoriler_Tablosu} SET {veri_sütunu} = @yeni_veri WHERE Kullanıcı = @kimlik";

        MySqlCommand komut = new MySqlCommand(kod, bağlantı);

        komut.Parameters.AddWithValue("@yeni_veri", yeni_veri);
        komut.Parameters.AddWithValue("@kimlik", kimlik);

        komut.ExecuteNonQuery();
        komut.Dispose();

        bağlantı.Close();
        bağlantı.Dispose();

    }

    public static bool SatırVar(string kullanıcı_kimliği, string konum_kimliği)
    {
        string cs = Bağlantı.bağlantı_dizisi;
        
        MySqlConnection bağlantı = new MySqlConnection(cs);
        bağlantı.Open();

        string kod = $"SELECT COUNT(Konum_Kimliği) FROM {Bağlantı.Favoriler_Tablosu} WHERE Kullanıcı = @kullanıcı_kimliği AND Konum_Kimliği = @konum_kimliği";

        MySqlCommand komut = new MySqlCommand(kod, bağlantı);

        komut.Parameters.AddWithValue("@kullanıcı_kimliği", kullanıcı_kimliği);
        komut.Parameters.AddWithValue("@konum_kimliği", konum_kimliği);

        int sonuc = int.Parse(komut.ExecuteScalar().ToString());

        komut.Dispose();
        return (sonuc >= 1);
    }

    public static bool SatırVarAçık(string kullanıcı_kimliği, string konum_kimliği, MySqlConnection açık_bağlantı)
    {
        string kod = $"SELECT COUNT(Konum_Kimliği) FROM {Bağlantı.Favoriler_Tablosu} WHERE Kullanıcı = @kullanıcı_kimliği AND Konum_Kimliği = @konum_kimliği";

        MySqlCommand komut = new MySqlCommand(kod, açık_bağlantı);

        komut.Parameters.AddWithValue("@kullanıcı_kimliği", kullanıcı_kimliği);
        komut.Parameters.AddWithValue("@konum_kimliği", konum_kimliği);

        int sonuc = int.Parse(komut.ExecuteScalar().ToString());

        komut.Dispose();
        return (sonuc >= 1);
    }

    public static bool FavoriEkle(string Kullanıcı_Kimliği, string Konum_Kimliği)
    {
        string cs = Bağlantı.bağlantı_dizisi;
        
        MySqlConnection bağlantı = new MySqlConnection(cs);
        bağlantı.Open();

        if(!SatırVarAçık(Kullanıcı_Kimliği, Konum_Kimliği, bağlantı))
        {
            if(KullanıcıFonksiyonları.VeriVarAçık("Kimlik", Kullanıcı_Kimliği, bağlantı) && HaritaFonksiyonları.VeriVarAçık("Kimlik", Konum_Kimliği, bağlantı))
            {
                string kod = $"INSERT INTO {Bağlantı.Favoriler_Tablosu}(Kullanıcı, Konum_Kimliği) VALUES(@kullanıcı_kimliği, @konum_kimliği);";

                MySqlCommand komut = new MySqlCommand(kod, bağlantı);

                komut.Parameters.AddWithValue("@kullanıcı_kimliği", Kullanıcı_Kimliği);
                komut.Parameters.AddWithValue("@konum_kimliği", Konum_Kimliği);

                komut.ExecuteNonQuery();
                komut.Dispose();

                bağlantı.Close();
                bağlantı.Dispose();

                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
        
    }

    public static bool FavorilerdenCikar(string Kullanıcı_Kimliği, string Konum_Kimliği)
    {
        string cs = Bağlantı.bağlantı_dizisi;
        
        MySqlConnection bağlantı = new MySqlConnection(cs);
        bağlantı.Open();

        if(SatırVarAçık(Kullanıcı_Kimliği, Konum_Kimliği, bağlantı))
        {
            string kod = $"DELETE FROM {Bağlantı.Favoriler_Tablosu} WHERE Kullanıcı = @kullanıcı_kimliği AND Konum_Kimliği = @konum_kimliği;";

            MySqlCommand komut = new MySqlCommand(kod, bağlantı);

            komut.Parameters.AddWithValue("@kullanıcı_kimliği", Kullanıcı_Kimliği);
            komut.Parameters.AddWithValue("@konum_kimliği", Konum_Kimliği);

            komut.ExecuteNonQuery();
            komut.Dispose();

            bağlantı.Close();
            bağlantı.Dispose();

            return true;
        }
        else
        {
            return false;
        }
        
    }

    public static List<String> FavorileriAl(string Kullanıcı_Kimliği)
    {
        List<String> Favori_Kimlikleri = new List<string>();
        
        string cs = Bağlantı.bağlantı_dizisi;

        MySqlConnection bağlantı = new MySqlConnection(cs);
        bağlantı.Open();

        string kod = $"SELECT Konum_Kimliği FROM {Bağlantı.Favoriler_Tablosu} WHERE Kullanıcı = @kullanıcı_kimliği";
        MySqlCommand komut = new MySqlCommand(kod, bağlantı);
        komut.Parameters.AddWithValue("@kullanıcı_kimliği", Kullanıcı_Kimliği);
        MySqlDataReader okuyucu =  komut.ExecuteReader();

        while(okuyucu.Read())
        {
            string Konum_Kimliği = okuyucu.GetString("Konum_Kimliği");
            Favori_Kimlikleri.Add(Konum_Kimliği);
        }

        return Favori_Kimlikleri;
    }
}