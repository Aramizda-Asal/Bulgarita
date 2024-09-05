using bulgarita.Models;
using bulgarita;
using MySql.Data.MySqlClient;
using System.Data;

namespace bulgarita.Services;

public static class HaritaFonksiyonları
{
    public static List<Harita> BölgelerinBilgileriniAl(string Bölge_Türü)
    {
        string cs = Bağlantı.bağlantı_dizisi;

        MySqlConnection bağlantı = new MySqlConnection(cs);
        bağlantı.Open();

        string kod = 
        $"SELECT * FROM {Bağlantı.Harita_Tablosu} WHERE Bölge_Türü = @bölge_türü;";

        MySqlCommand komut = new MySqlCommand(kod, bağlantı);

        komut.Parameters.AddWithValue("@bölge_türü", Bölge_Türü);

        Models.Harita haritaNoktalar = new Models.Harita();
        
        MySqlDataReader okuyucu =  komut.ExecuteReader();
        List<Harita> BölgeListe = new List<Harita>();

        while(okuyucu.Read())
        {
            double EnlemDrc = okuyucu.GetDouble("EnlemDrc");
            double BoylamDrc = okuyucu.GetDouble("BoylamDrc");
            string Mevcut_İsim = okuyucu.GetString("Mevcut_İsim");
            //string KirilBulgr_İsim = okuyucu.GetString("KirilBulgr_İsim");
            //string Türkçe_İsim = okuyucu.GetString("Türkçe_İsim");
            string Üst_Bölge = okuyucu.GetString("Üst_Bölge");
            //string Bölge_Türü = okuyucu.GetString("Bölge_Türü");
            string Kimlik = okuyucu.GetString("Kimlik");

            Harita bölge = 
            new Harita(EnlemDrc,BoylamDrc,Mevcut_İsim,/*KirilBulgr_İsim,Türkçe_İsim,*/Üst_Bölge,Bölge_Türü,Kimlik);
            BölgeListe.Add(bölge);
        }

        foreach (Harita bölge in BölgeListe)
        {
            Console.WriteLine(bölge.Mevcut_İsim);
        }

        okuyucu.Close();
        
        komut.Dispose();

        return BölgeListe;
    }
}