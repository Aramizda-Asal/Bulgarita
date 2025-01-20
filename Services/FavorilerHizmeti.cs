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

    public static bool FavoriEkle(string Kullanıcı_Kimliği, string Konum_Kimliği)
    {
        string cs = Bağlantı.bağlantı_dizisi;
        
        MySqlConnection bağlantı = new MySqlConnection(cs);
        bağlantı.Open();

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
}