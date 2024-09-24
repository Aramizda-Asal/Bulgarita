using bulgarita.Models;
using bulgarita;
using MySql.Data.MySqlClient;
using System.Data;

namespace bulgarita.Services;

public static class HaritaFonksiyonları
{
    public static List<Harita> BölgelerinBilgileriniAl(string bölge_türü)
    {
        string cs = Bağlantı.bağlantı_dizisi;

        MySqlConnection bağlantı = new MySqlConnection(cs);
        bağlantı.Open();

        string kod = 
        $"SELECT * FROM {Bağlantı.Harita_Tablosu} WHERE Bölge_Türü = @bölge_türü;";

        MySqlCommand komut = new MySqlCommand(kod, bağlantı);

        komut.Parameters.AddWithValue("@bölge_türü", bölge_türü);

        Models.Harita haritaNoktalar = new Models.Harita();
        
        MySqlDataReader okuyucu =  komut.ExecuteReader();
        List<Harita> BölgeListe = new List<Harita>();

        while(okuyucu.Read())
        {
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
}