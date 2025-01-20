using bulgarita.Models;
using bulgarita;
using MySql.Data.MySqlClient;
using System.Data;
using System.Text;

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

    public static bool BölgeninBilgileriniKoy(double EnlemDrc, double BoylamDrc, string Bulgarca_Latin_İsim, string Bulgarca_Kiril_İsim, string Türkçe_İsim,
                                                string Osmanlıca_İsim, string Bölge_Türü, string Üst_Bölge, string Kimlik)
    {
        string cs = Bağlantı.bağlantı_dizisi;

        MySqlConnection bağlantı = new MySqlConnection(cs);
        bağlantı.Open();

        try
        {
            string kod = 
            $"INSERT INTO {Bağlantı.Harita_Tablosu} VALUES (@Enlem, @Boylam, @BulgarcaL, @BulgarcaK, @Türkçe, @Osmanlıca, @BölgeTürü, @ÜstBölge, @Kimlik);";

            MySqlCommand komut = new MySqlCommand(kod, bağlantı);

            komut.Parameters.AddWithValue("@Enlem",EnlemDrc);
            komut.Parameters.AddWithValue("@Boylam",BoylamDrc);
            komut.Parameters.AddWithValue("@BulgarcaL",Bulgarca_Latin_İsim);
            komut.Parameters.AddWithValue("@BulgarcaK",Bulgarca_Kiril_İsim);
            komut.Parameters.AddWithValue("@Türkçe",Türkçe_İsim);
            komut.Parameters.AddWithValue("@Osmanlıca",Osmanlıca_İsim);
            komut.Parameters.AddWithValue("@BölgeTürü",Bölge_Türü);
            komut.Parameters.AddWithValue("@ÜstBölge",Üst_Bölge);
            komut.Parameters.AddWithValue("@Kimlik",Kimlik);
            
            komut.ExecuteNonQuery();
            komut.Dispose();

            bağlantı.Close();
            bağlantı.Dispose();
            
            return true;
        }
        catch
        {
            return false;
        }

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

    public static bool VeriVarAçık(string sütun, string veri, MySqlConnection açık_bağlantı)
    {
        string kod = $"SELECT COUNT({sütun}) FROM {Bağlantı.Harita_Tablosu} WHERE {sütun} = @veri";

        MySqlCommand komut = new MySqlCommand(kod, açık_bağlantı);

        komut.Parameters.AddWithValue("@veri", veri);

        int sonuc = int.Parse(komut.ExecuteScalar().ToString());

        komut.Dispose();
        return (sonuc >= 1);
    }
}