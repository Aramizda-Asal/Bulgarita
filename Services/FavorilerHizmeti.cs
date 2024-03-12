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
}