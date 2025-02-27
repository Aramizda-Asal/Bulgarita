// Copyright (C) 2025 Güneş Balcı, Habil Tataroğulları, Yusuf Kozan

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
        
        MySqlDataReader okuyucu =  komut.ExecuteReader();
        List<Harita> sonuçlar = new List<Harita>();

        while(okuyucu.Read())
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
                    okuyucu["Kimlik"].ToString()
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
            if(okuyucu.IsDBNull("Üst_Bölge"))
            {
                continue;
            }
            double EnlemDrc = okuyucu.GetDouble("EnlemDrc");
            double BoylamDrc = okuyucu.GetDouble("BoylamDrc");
            string Bulgarca_Latin_İsim = okuyucu.GetString("Bulgarca_Latin_İsim");
            string Bulgarca_Kiril_İsim = okuyucu.GetString("Bulgarca_Kiril_İsim");
            string Türkçe_İsim = okuyucu.GetString("Türkçe_İsim");
            string Osmanlıca_İsim = okuyucu.GetString("Osmanlıca_İsim");
            string Bölge_Türü = okuyucu.GetString("Bölge_Türü");
            string Üst_Bölge = okuyucu.GetString("Üst_Bölge");
            string Kimlik = okuyucu.GetString("Kimlik");

            Harita bölge = 
            new Harita(EnlemDrc,BoylamDrc,Bulgarca_Latin_İsim,Bulgarca_Kiril_İsim,Türkçe_İsim,Osmanlıca_İsim,Bölge_Türü,Üst_Bölge,Kimlik);
            BölgeListe.Add(bölge);
        }

        okuyucu.Close();
        
        komut.Dispose();

        return BölgeListe;
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
                    "Bölge_Türü = @tür, Üst_Bölge = @üst_bölge, Kimlik = @kimlik " +
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

    public static bool BölgeBilgileriniDeğis(string kimlik, string veri_sütunu, string yeni_veri)
    {
        string cs = Bağlantı.bağlantı_dizisi;

        MySqlConnection bağlantı = new MySqlConnection(cs);
        bağlantı.Open();

        try 
        {
            string kod = $"Update {Bağlantı.Harita_Tablosu} SET {veri_sütunu} = @yeni_veri WHERE Kimlik = @kimlik";

            MySqlCommand komut = new MySqlCommand(kod, bağlantı);

            komut.Parameters.AddWithValue("@yeni_veri", yeni_veri);
            komut.Parameters.AddWithValue("@kimlik", kimlik);

            komut.ExecuteNonQuery();
            komut.Dispose();

            return true;
        }
        catch
        {
            return false;
        }

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