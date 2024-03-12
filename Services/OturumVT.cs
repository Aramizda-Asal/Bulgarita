using System;
using System.Text;
using bulgarita.Models;
using MySql.Data.MySqlClient;

namespace bulgarita.Services;

public static class OturumVT
{
    public static bool OturumAç(string kullanıcı_kimliği, string oturum_kimliği, double süre_saat)
    {
        bool başarılı = false;
        StringBuilder komut_metni = new StringBuilder();
        komut_metni.Append($"INSERT INTO {Bağlantı.Oturum_Tablosu}(Kullanıcı, Kimlik, Başlangıç, Bitiş) ");
        komut_metni.Append("VALUES (@kullanıcı, @kimlik, @başlangıç, @bitiş);");

        DateTime başlangıç = DateTime.Now;

        MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizisi);
        bağlantı.Open();

        MySqlCommand komut = new MySqlCommand(komut_metni.ToString(), bağlantı);
        komut.Parameters.AddWithValue("@kullanıcı", kullanıcı_kimliği);
        komut.Parameters.AddWithValue("@kimlik", oturum_kimliği);
        komut.Parameters.AddWithValue("@başlangıç", başlangıç.ToString("yyyyMMddHHmmss"));
        komut.Parameters.AddWithValue("@bitiş", başlangıç.AddHours(süre_saat).ToString("yyyyMMddHHmmss"));
        
        try
        {
            komut.ExecuteNonQuery();
            başarılı = true;
        }
        catch
        {
            başarılı = false;
        }

        komut.Dispose();
        bağlantı.Close();
        bağlantı.Dispose();
        return başarılı;
    }
    public static bool OturumAç(string kullanıcı_kimliği, string oturum_kimliği,
                                double süre_saat, MySqlConnection açık_bağlantı)
    {
        bool başarılı = false;
        StringBuilder komut_metni = new StringBuilder();
        komut_metni.Append($"INSERT INTO {Bağlantı.Oturum_Tablosu}(Kullanıcı, Kimlik, Başlangıç, Bitiş) ");
        komut_metni.Append("VALUES (@kullanıcı, @kimlik, @başlangıç, @bitiş);");

        DateTime başlangıç = DateTime.Now;

        MySqlCommand komut = new MySqlCommand(komut_metni.ToString(), açık_bağlantı);
        komut.Parameters.AddWithValue("@kullanıcı", kullanıcı_kimliği);
        komut.Parameters.AddWithValue("@kimlik", oturum_kimliği);
        komut.Parameters.AddWithValue("@başlangıç", başlangıç.ToString("yyyyMMddHHmmss"));
        komut.Parameters.AddWithValue("@bitiş", başlangıç.AddHours(süre_saat).ToString("yyyyMMddHHmmss"));
        
        try
        {
            komut.ExecuteNonQuery();
            başarılı = true;
        }
        catch
        {
            başarılı = false;
        }

        komut.Dispose();
        return başarılı;
    }

    public static bool OturumKaydet(Models.Oturum oturum)
    {
        bool başarılı = false;
        StringBuilder komut_metni = new StringBuilder();
        komut_metni.Append($"INSERT INTO {Bağlantı.Oturum_Tablosu}(Kullanıcı, Kimlik, Başlangıç, Bitiş) ");
        komut_metni.Append("VALUES (@kullanıcı, @kimlik, @başlangıç, @bitiş);");

        MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizisi);
        bağlantı.Open();

        MySqlCommand komut = new MySqlCommand(komut_metni.ToString(), bağlantı);
        komut.Parameters.AddWithValue("@kullanıcı", oturum.Kullanıcı);
        komut.Parameters.AddWithValue("@kimlik", oturum.Kimlik);
        komut.Parameters.AddWithValue("@başlangıç", oturum.Başlangıç.ToString("yyyyMMddHHmmss"));
        komut.Parameters.AddWithValue("@bitiş", oturum.Bitiş.ToString("yyyyMMddHHmmss"));
        
        try
        {
            komut.ExecuteNonQuery();
            başarılı = true;
        }
        catch
        {
            başarılı = false;
        }

        komut.Dispose();
        bağlantı.Close();
        bağlantı.Dispose();
        return başarılı;
    }
    public static bool OturumKaydet(Models.Oturum oturum, MySqlConnection açık_bağlantı)
    {
        bool başarılı = false;
        StringBuilder komut_metni = new StringBuilder();
        komut_metni.Append($"INSERT INTO {Bağlantı.Oturum_Tablosu}(Kullanıcı, Kimlik, Başlangıç, Bitiş) ");
        komut_metni.Append("VALUES (@kullanıcı, @kimlik, @başlangıç, @bitiş);");

        MySqlCommand komut = new MySqlCommand(komut_metni.ToString(), açık_bağlantı);
        komut.Parameters.AddWithValue("@kullanıcı", oturum.Kullanıcı);
        komut.Parameters.AddWithValue("@kimlik", oturum.Kimlik);
        komut.Parameters.AddWithValue("@başlangıç", oturum.Başlangıç.ToString("yyyyMMddHHmmss"));
        komut.Parameters.AddWithValue("@bitiş", oturum.Bitiş.ToString("yyyyMMddHHmmss"));
        
        try
        {
            komut.ExecuteNonQuery();
            başarılı = true;
        }
        catch
        {
            başarılı = false;
        }

        komut.Dispose();
        return başarılı;
    }

    public static bool KimlikVar(string oturum_kimliği)
    {
        StringBuilder komut_metni = new StringBuilder();
        komut_metni.Append($"SELECT COUNT(Kimlik) FROM {Bağlantı.Oturum_Tablosu} ");
        komut_metni.Append("WHERE Kimlik = @kimlik;");

        MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizisi);
        bağlantı.Open();

        MySqlCommand komut = new MySqlCommand(komut_metni.ToString(), bağlantı);
        komut.Parameters.AddWithValue("@kimlik", oturum_kimliği);

        int nicelik = 0;
        
        bool sorunsuz = int.TryParse(komut.ExecuteScalar().ToString(), out nicelik);

        komut.Dispose();
        bağlantı.Close();
        bağlantı.Dispose();

        return (nicelik > 0) || !sorunsuz;
    }
    public static bool KimlikVar(string oturum_kimliği, MySqlConnection açık_bağlantı)
    {
        StringBuilder komut_metni = new StringBuilder();
        komut_metni.Append($"SELECT COUNT(Kimlik) FROM {Bağlantı.Oturum_Tablosu} ");
        komut_metni.Append("WHERE Kimlik = @kimlik;");

        MySqlCommand komut = new MySqlCommand(komut_metni.ToString(), açık_bağlantı);
        komut.Parameters.AddWithValue("@kimlik", oturum_kimliği);

        int nicelik = 0;
        
        bool sorunsuz = int.TryParse(komut.ExecuteScalar().ToString(), out nicelik);

        komut.Dispose();

        return (nicelik > 0) || !sorunsuz;
    }
}