using System;
using System.Text;
using bulgarita.Models;
using MySql.Data.MySqlClient;

namespace bulgarita.Services;

public static class OturumFonksiyonları
{
    public static Models.Oturum OturumBaşlat(string kullanıcı_adı, string parola)
    {
        MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizisi);
        bağlantı.Open();

        if (KullanıcıFonksiyonları.GirişBilgileriDoğru(kullanıcı_adı, parola, bağlantı))
        {
            StringBuilder komut_metni = new StringBuilder();
            komut_metni.Append($"SELECT Kimlik FROM {Bağlantı.Kullanıcı_Tablosu} ");
            komut_metni.Append("WHERE Kullanıcı_Adı = @kullanıcı_adı;");

            MySqlCommand komut = new MySqlCommand(komut_metni.ToString(), bağlantı);
            komut.Parameters.AddWithValue("@kullanıcı_adı", kullanıcı_adı);
            string kullanıcı_kimliği = komut.ExecuteScalar().ToString();
            komut.Dispose();
            komut_metni.Clear();

            Models.Oturum yeni_oturum = new Oturum(kullanıcı_kimliği, 8);
            if (OturumVT.OturumKaydet(yeni_oturum, bağlantı))
            {
                bağlantı.Close();
                bağlantı.Dispose();
                return yeni_oturum;
            }
        }
        
        bağlantı.Close();
        bağlantı.Dispose();
        return null;
    }
}