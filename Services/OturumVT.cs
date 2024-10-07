using System;
using System.Text;
using System.Globalization;
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
        komut.Parameters.AddWithValue("@kullanıcı", oturum.Kullanıcı.Kimlik);
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
        komut.Parameters.AddWithValue("@kullanıcı", oturum.Kullanıcı.Kimlik);
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
    
    public static bool OturumKapat(string oturum_kimliği)
    {
        bool sonuç = false;

        MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizisi);
        bağlantı.Open();

        StringBuilder komut_metni = new StringBuilder();
        komut_metni.Append($"UPDATE {Bağlantı.bağlantı_dizisi} ");
        komut_metni.Append("SET Bitiş = @şimdi WHERE Kimlik = @kimlik;");
        MySqlCommand komut = new MySqlCommand(komut_metni.ToString(), bağlantı);
        komut.Parameters.AddWithValue("@şimdi", DateTime.Now.ToString("yyyyMMddHHmmss"));
        komut.Parameters.AddWithValue("@kimlik", oturum_kimliği);
        
        try
        {
            komut.ExecuteNonQuery();
            sonuç = true;
        }
        catch
        {}

        komut.Dispose();
        bağlantı.Close();
        bağlantı.Dispose();

        return sonuç;
    }
    public static bool OturumKapat(string oturum_kimliği, MySqlConnection açık_bağlantı)
    {
        bool sonuç = false;

        StringBuilder komut_metni = new StringBuilder();
        komut_metni.Append($"UPDATE {Bağlantı.Oturum_Tablosu} ");
        komut_metni.Append("SET Bitiş = @şimdi WHERE Kimlik = @kimlik;");
        MySqlCommand komut = new MySqlCommand(komut_metni.ToString(), açık_bağlantı);
        komut.Parameters.AddWithValue("@şimdi", DateTime.Now.ToString("yyyyMMddHHmmss"));
        komut.Parameters.AddWithValue("@kimlik", oturum_kimliği);
        
        try
        {
            komut.ExecuteNonQuery();
            sonuç = true;
        }
        catch
        {}

        komut.Dispose();

        return sonuç;
    }

    public static bool OturumAçık(string kullanıcı_kimliği, string oturum_kimliği)
    {
        MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizisi);
        bağlantı.Open();

        StringBuilder komut_metni = new StringBuilder();

        komut_metni.Append($"SELECT COUNT(Kimlik) FROM {Bağlantı.Oturum_Tablosu} ");
        komut_metni.Append("WHERE Kullanıcı = @kullanıcı AND Kimlik = @kimlik;");
        MySqlCommand komut = new MySqlCommand(komut_metni.ToString(), bağlantı);
        komut.Parameters.AddWithValue("@kullanıcı", kullanıcı_kimliği);
        komut.Parameters.AddWithValue("@kimlik", oturum_kimliği);

        int nicelik = int.Parse(komut.ExecuteScalar().ToString());
        komut.Dispose();
        komut_metni.Clear();

        if (nicelik != 1)
        {
            komut.Dispose();
            bağlantı.Close();
            bağlantı.Dispose();
            return false;
        }
        
        komut_metni.Append($"SELECT Başlangıç, Bitiş FROM {Bağlantı.Oturum_Tablosu} ");
        komut_metni.Append("WHERE Kullanıcı = @kullanıcı AND Kimlik = @kimlik;");
        komut = new MySqlCommand(komut_metni.ToString(), bağlantı);
        komut.Parameters.AddWithValue("@kullanıcı", kullanıcı_kimliği);
        komut.Parameters.AddWithValue("@kimlik", oturum_kimliği);

        DateTime baş = new DateTime();
        DateTime son = new DateTime();
        MySqlDataReader okuyucu = komut.ExecuteReader();
        while (okuyucu.Read())
        {
            DateTime.TryParseExact
            (
                okuyucu["Başlangıç"].ToString(),
                "yyyyMMddHHmmss",
                Yerelleştirme.Yöre,
                DateTimeStyles.None,
                out baş
            );
            DateTime.TryParseExact
            (
                okuyucu["Bitiş"].ToString(),
                "yyyyMMddHHmmss",
                Yerelleştirme.Yöre,
                DateTimeStyles.None,
                out son
            );
        }
        okuyucu.Close();
        komut.Dispose();
        bağlantı.Close();
        bağlantı.Dispose();

        bool baş_önce = DateTime.Compare(baş, son) < 0;
        bool son_gelmemiş = DateTime.Compare(son, DateTime.Now) > 0;
        
        return (baş_önce && son_gelmemiş);
    }
    public static bool OturumAçık(string kullanıcı_kimliği,
                        string oturum_kimliği, MySqlConnection açık_bağlantı)
    {
        StringBuilder komut_metni = new StringBuilder();

        komut_metni.Append($"SELECT COUNT(Kimlik) FROM {Bağlantı.Oturum_Tablosu} ");
        komut_metni.Append("WHERE Kullanıcı = @kullanıcı AND Kimlik = @kimlik;");
        MySqlCommand komut = new MySqlCommand(komut_metni.ToString(), açık_bağlantı);
        komut.Parameters.AddWithValue("@kullanıcı", kullanıcı_kimliği);
        komut.Parameters.AddWithValue("@kimlik", oturum_kimliği);

        int nicelik = int.Parse(komut.ExecuteScalar().ToString());
        komut.Dispose();
        komut_metni.Clear();

        if (nicelik != 1)
        {
            komut.Dispose();
            return false;
        }
        
        komut_metni.Append($"SELECT Başlangıç, Bitiş FROM {Bağlantı.Oturum_Tablosu} ");
        komut_metni.Append("WHERE Kullanıcı = @kullanıcı AND Kimlik = @kimlik;");
        komut = new MySqlCommand(komut_metni.ToString(), açık_bağlantı);
        komut.Parameters.AddWithValue("@kullanıcı", kullanıcı_kimliği);
        komut.Parameters.AddWithValue("@kimlik", oturum_kimliği);

        DateTime baş = new DateTime();
        DateTime son = new DateTime();
        MySqlDataReader okuyucu = komut.ExecuteReader();
        while (okuyucu.Read())
        {
            DateTime.TryParseExact
            (
                okuyucu["Başlangıç"].ToString(),
                "yyyyMMddHHmmss",
                Yerelleştirme.Yöre,
                DateTimeStyles.None,
                out baş
            );
            DateTime.TryParseExact
            (
                okuyucu["Bitiş"].ToString(),
                "yyyyMMddHHmmss",
                Yerelleştirme.Yöre,
                DateTimeStyles.None,
                out son
            );
        }
        okuyucu.Close();
        komut.Dispose();

        bool baş_önce = DateTime.Compare(baş, son) < 0;
        bool son_gelmemiş = DateTime.Compare(son, DateTime.Now) > 0;
        
        return baş_önce && son_gelmemiş;
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