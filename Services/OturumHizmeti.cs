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
    
    public static bool OturumBitir(string kullanıcı_kimliği, string oturum_kimliği)
    {
        MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizisi);
        bağlantı.Open();

        bool oturum_açık = OturumVT.OturumAçık(kullanıcı_kimliği, oturum_kimliği, bağlantı);
        if (!oturum_açık)
        {
            bağlantı.Close();
            bağlantı.Dispose();
            return true;
        }

        bool kapandı = OturumVT.OturumKapat(oturum_kimliği, bağlantı);
        bağlantı.Close();
        bağlantı.Dispose();
        return kapandı;
    }
}