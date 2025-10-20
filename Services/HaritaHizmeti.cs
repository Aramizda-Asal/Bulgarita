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
using System.Data;
using System.Text;

namespace bulgarita.Services;

public static class HaritaFonksiyonları
{
    /**
    * <summary>
    * Belirtilen türdeki noktaları bir List'te bir araya getirir.
    * </summary>
    *
    * <param name="bölge_türü">Aranan noktaların bölge türü</param>
    *
    * <returns>
    * Belirtilen bölge türündeki noktaları içeren bir List.
    * </returns>
    */
    public static List<Harita> BölgelerinBilgileriniAl(string bölge_türü)
    {
        string cs = Bağlantı.bağlantı_dizisi;

        MySqlConnection bağlantı = new MySqlConnection(cs);
        bağlantı.Open();

        string kod =
        $"SELECT * FROM {Bağlantı.Harita_Tablosu} WHERE Bölge_Türü = @bölge_türü;";

        MySqlCommand komut = new MySqlCommand(kod, bağlantı);
        komut.Parameters.AddWithValue("@bölge_türü", bölge_türü);

        MySqlDataReader okuyucu = komut.ExecuteReader();
        List<Harita> sonuçlar = new List<Harita>();

        while (okuyucu.Read())
        {
            try
            {
                Harita bölge = new Harita(
                    double.Parse(okuyucu["EnlemDrc"].ToString()),
                    double.Parse(okuyucu["BoylamDrc"].ToString()),
                    okuyucu["Bulgarca_Latin_İsim"].ToString(),
                    okuyucu["Bulgarca_Kiril_İsim"].ToString(),
                    okuyucu["Türkçe_İsim"].ToString(),
                    okuyucu["Osmanlıca_İsim"].ToString(),
                    okuyucu["Bölge_Türü"].ToString(),
                    okuyucu["Üst_Bölge"].ToString(),
                    okuyucu["Kimlik"].ToString(),
                    okuyucu["Aciklama"].ToString()
                );
                sonuçlar.Add(bölge);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                continue;
            }
        }

        okuyucu.Close();
        komut.Dispose();
        bağlantı.Close();
        bağlantı.Dispose();

        return sonuçlar;
    }

    
    //Bölgeleri eklerken türe göre önem kalmadığı için artık bölge türü almadan yapıyorum yukardakini silmek gerekir mi bilemedim.
    public static List<Harita> BölgelerinBilgileriniAl()
    {
        string cs = Bağlantı.bağlantı_dizisi;

        MySqlConnection bağlantı = new MySqlConnection(cs);
        bağlantı.Open();

        string kod = 
        $"SELECT * FROM {Bağlantı.Harita_Tablosu};";

        MySqlCommand komut = new MySqlCommand(kod, bağlantı);

        Models.Harita haritaNoktalar = new Models.Harita();
        
        MySqlDataReader okuyucu =  komut.ExecuteReader();
        List<Harita> BölgeListe = new List<Harita>();

        while(okuyucu.Read())
        {
            try
            {
                double EnlemDrc = double.Parse(okuyucu["EnlemDrc"].ToString());
                double BoylamDrc = double.Parse(okuyucu["BoylamDrc"].ToString());
                string Bulgarca_Latin_İsim = okuyucu["Bulgarca_Latin_İsim"].ToString();
                string Bulgarca_Kiril_İsim = okuyucu["Bulgarca_Kiril_İsim"].ToString();
                string Türkçe_İsim = okuyucu["Türkçe_İsim"].ToString();
                string Osmanlıca_İsim = okuyucu["Osmanlıca_İsim"].ToString();
                string Bölge_Türü = okuyucu["Bölge_Türü"].ToString();
                string Üst_Bölge = okuyucu["Üst_Bölge"].ToString();
                string Kimlik = okuyucu["Kimlik"].ToString();
                string Aciklama = okuyucu["Aciklama"].ToString();

                Harita bölge = 
                new Harita(EnlemDrc,BoylamDrc,Bulgarca_Latin_İsim,Bulgarca_Kiril_İsim,Türkçe_İsim,Osmanlıca_İsim,Bölge_Türü,Üst_Bölge,Kimlik,Aciklama);
                BölgeListe.Add(bölge);
            }
            catch
            {
                continue;
            }
        }

        okuyucu.Close();
        
        komut.Dispose();
        bağlantı.Close();
        bağlantı.Dispose();

        return BölgeListe;
    }

    public static Harita BölgeninBilgileriniAl(string nokta_kimliği)
    {
        string cs = Bağlantı.bağlantı_dizisi;

        MySqlConnection bağlantı = new MySqlConnection(cs);
        bağlantı.Open();

        string kod =
        $"SELECT * FROM {Bağlantı.Harita_Tablosu} WHERE Kimlik = @kimlik;";

        MySqlCommand komut = new MySqlCommand(kod, bağlantı);
        komut.Parameters.AddWithValue("@kimlik", nokta_kimliği);

        MySqlDataReader okuyucu = komut.ExecuteReader();
        Harita sonuç = null;
        List<Harita> sonuçlar = new List<Harita>();

        if (okuyucu.Read())
        {
            try
            {
                sonuç = new Harita(
                    double.Parse(okuyucu["EnlemDrc"].ToString()),
                    double.Parse(okuyucu["BoylamDrc"].ToString()),
                    okuyucu["Bulgarca_Latin_İsim"].ToString(),
                    okuyucu["Bulgarca_Kiril_İsim"].ToString(),
                    okuyucu["Türkçe_İsim"].ToString(),
                    okuyucu["Osmanlıca_İsim"].ToString(),
                    okuyucu["Bölge_Türü"].ToString(),
                    okuyucu["Üst_Bölge"].ToString(),
                    okuyucu["Kimlik"].ToString(),
                    okuyucu["Aciklama"].ToString()
                );
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        okuyucu.Close();
        komut.Dispose();
        bağlantı.Close();
        bağlantı.Dispose();

        return sonuç;
    }

    public static bool YeniBölgeKaydet(Harita yeni)
    {
        if (yeni == null)
        {
            return false;
        }

        MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizisi);
        bağlantı.Open();

        bool başarılı = false;

        try
        {
            string kod = $"INSERT INTO {Bağlantı.Harita_Tablosu} VALUES ( " +
                "@enlem, @boylam, @bulgarca_latin, @bulgarca_kiril, @türkçe, " +
                "@osmanlıca, @tür, @üst_bölge, @kimlik);";
            
            MySqlCommand komut = new MySqlCommand(kod, bağlantı);
            komut.Parameters.AddWithValue("@enlem", yeni.EnlemDrc);
            komut.Parameters.AddWithValue("@boylam", yeni.BoylamDrc);
            komut.Parameters.AddWithValue("@bulgarca_latin", yeni.Bulgarca_Latin_İsim);
            komut.Parameters.AddWithValue("@bulgarca_kiril", yeni.Bulgarca_Kiril_İsim);
            komut.Parameters.AddWithValue("@türkçe", yeni.Türkçe_İsim);
            komut.Parameters.AddWithValue("@osmanlıca", yeni.Osmanlıca_İsim);
            komut.Parameters.AddWithValue("@tür", yeni.Bölge_Türü);
            komut.Parameters.AddWithValue("@üst_bölge", yeni.Üst_Bölge);
            komut.Parameters.AddWithValue("@kimlik", yeni.Kimlik);

            komut.ExecuteNonQuery();
            komut.Dispose();
            başarılı = true;
        }
        catch
        {}

        bağlantı.Close();
        bağlantı.Dispose();
        return başarılı;
    }

    public static bool BölgeBilgileriniGüncelle(Models.Harita nokta)
    {
        if (nokta == null)
        {
            return false;
        }

        MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizisi);
        bağlantı.Open();

        bool başarılı = false;

        try
        {
            string kod = $"UPDATE {Bağlantı.Harita_Tablosu} SET " +
                    "EnlemDrc = @enlem, BoylamDrc = @boylam, Bulgarca_Latin_İsim = @bulgarca_latin, " +
                    "Bulgarca_Kiril_İsim = @bulgarca_kiril, Türkçe_İsim = @türkçe, Osmanlıca_İsim = @osmanlıca, " +
                    "Bölge_Türü = @tür, Üst_Bölge = @üst_bölge, Kimlik = @kimlik, Aciklama = @aciklama " +
                    "WHERE Kimlik = @kimlik;"; 
        
            MySqlCommand komut = new MySqlCommand(kod, bağlantı);
            komut.Parameters.AddWithValue("@enlem", nokta.EnlemDrc);
            komut.Parameters.AddWithValue("@boylam", nokta.BoylamDrc);
            komut.Parameters.AddWithValue("@bulgarca_latin", nokta.Bulgarca_Latin_İsim);
            komut.Parameters.AddWithValue("@bulgarca_kiril", nokta.Bulgarca_Kiril_İsim);
            komut.Parameters.AddWithValue("@türkçe", nokta.Türkçe_İsim);
            komut.Parameters.AddWithValue("@osmanlıca", nokta.Osmanlıca_İsim);
            komut.Parameters.AddWithValue("@tür", nokta.Bölge_Türü);
            komut.Parameters.AddWithValue("@üst_bölge", nokta.Üst_Bölge);
            komut.Parameters.AddWithValue("@kimlik", nokta.Kimlik);
            komut.Parameters.AddWithValue("@aciklama", nokta.Aciklama);

            komut.ExecuteNonQuery();
            komut.Dispose();
            başarılı = true;
        }
        catch
        {}

        bağlantı.Close();
        bağlantı.Dispose();

        return başarılı;
    }

    /**
    * <summary>
    * Girilen bölge türünün bulunduğu düzeyin indisini bulur.
    * </summary>
    *
    * <param name="bölge_türü">Düzeyi aranan bölge türü</param>
    *
    * <returns>
    * Girilen bölge türü uygunsa UygunTürler'in ilk boyutundaki indisi döndürür.
    * Uygun değilse <c>-1</c> döndürür.
    * </returns>
    *
    * <seealso cref="bulgarita.Models.Harita.UygunTürler" />
    */
    public static int BölgeTürüDüzeyi(string bölge_türü)
    {
        if (String.IsNullOrWhiteSpace(bölge_türü))
        {
            return -1;
        }
        for (int a = 0; a < Harita.UygunTürler.Length; a++)
        {
            if (Harita.UygunTürler[a].Contains<string>(bölge_türü))
            {
                return a;
            }
        }
        return -1;
    }

    public static bool VeriVarAçık(string sütun, string veri, MySqlConnection açık_bağlantı)
    {
        string kod = $"SELECT COUNT({sütun}) FROM {Bağlantı.Harita_Tablosu} WHERE {sütun} = @veri";

        MySqlCommand komut = new MySqlCommand(kod, açık_bağlantı);

        komut.Parameters.AddWithValue("@veri", veri);

        int sonuc = int.Parse(komut.ExecuteScalar().ToString());

        komut.Dispose();
        return (sonuc >= 1);
    }

    /**
    * <summary>
    * Belirtilen kimliğe sahip bir nokta bulunup bulunmadığını denetler.
    * </summary>
    * <remarks>
    * <para>
    * Aynı sınıfta bulunan
    * <see cref="bulgarita.Services.HaritaFonksiyonları.VeriVarAçık(string, string, MySqlConnection)">
    * VeriVarAçık
    * </see>
    * metodunu kullanır.
    * </para>
    * </remarks>
    *
    * <param name="kimlik">Kullanım durumu sorgulanan nokta kimliği</param>
    *
    * <returns>
    * Verilen kimliğe sahip bir nokta varsa <c>true</c>,
    * yoksa <c>false</c>.
    * </returns>
    */
    public static bool KimlikKullanımda(string kimlik)
    {
        MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizisi);
        bağlantı.Open();
        bool sonuç = VeriVarAçık("Kimlik", kimlik, bağlantı);
        bağlantı.Close();
        bağlantı.Dispose();

        return sonuç;
    }

    public static bool BölgeSil(string Kimlik)
    {
        try
        {
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizisi);
            bağlantı.Open();
            
            if(!VeriVarAçık("Üst_Bölge", Kimlik, bağlantı))
            {
                string kod = $"DELETE FROM {Bağlantı.Harita_Tablosu} WHERE Kimlik = @kimlik;";

                MySqlCommand komut = new MySqlCommand(kod,bağlantı);
                komut.Parameters.AddWithValue("@kimlik",Kimlik);

                komut.ExecuteNonQuery();

                komut.Dispose();
                //Kullanıcı kimliği almayan favori silme fonksiyonu yazılmalı.

                bağlantı.Close();
                bağlantı.Dispose();

                return true;
            }
            else
            {
                bağlantı.Close();
                bağlantı.Dispose();

                return false;
            }
        }
        catch
        {
            return false;
        }
    }
}