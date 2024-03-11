using bulgarita.Models;
using bulgarita;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;

namespace bulgarita.Services;

public static class KullanıcıFonksiyonları
{
    private static string TabloAdı = "kullanıcı";
    //Buradaki tablo ismi ayarlar dosyasından çekilecektir.

    public static bool BilgiDoğru(string kimlik, string veri, string veri_sütunu, MySqlConnection açık_bağlantı)
    {
        //Burdaki tablo ismi ayarlar dosyasından çekilecektir.
        string tablo = "kullanıcı";

        string kod = $"SELECT COUNT(kimlik) FROM {tablo} WHERE {veri_sütunu} = @veri AND kimlik = @kimlik";

        MySqlCommand komut = new MySqlCommand(kod, açık_bağlantı);

        komut.Parameters.AddWithValue("@veri", veri);
        komut.Parameters.AddWithValue("@kimlik", kimlik);

        int sonuc = int.Parse(komut.ExecuteScalar().ToString());

        komut.Dispose();
        return (sonuc >= 1);
    }

    public static bool VeriVar(string sütun, string veri)
    {
        string cs = Bağlantı.bağlantı_dizisi;
        MySqlConnection bağlantı = new MySqlConnection(cs);
        bağlantı.Open();

        //Burdaki tablo ismi ayarlar dosyasından çekilecektir.
        string tablo = "kullanıcı";

        string kod = $"SELECT COUNT({sütun}) FROM {tablo} WHERE {sütun} = @veri";

        MySqlCommand komut = new MySqlCommand(kod, bağlantı);

        komut.Parameters.AddWithValue("@veri", veri);

        int sonuc = int.Parse(komut.ExecuteScalar().ToString());

        komut.Dispose();
        bağlantı.Close();
        bağlantı.Dispose();

        return (sonuc >= 1);
    }

    public static bool VeriVarAçık(string sütun, string veri, MySqlConnection açık_bağlantı)
    {
         //Burdaki tablo ismi ayarlar dosyasından çekilecektir.
        string tablo = "kullanıcı";

        string kod = $"SELECT COUNT({sütun}) FROM {tablo} WHERE {sütun} = @veri";

        MySqlCommand komut = new MySqlCommand(kod, açık_bağlantı);

        komut.Parameters.AddWithValue("@veri", veri);

        int sonuc = int.Parse(komut.ExecuteScalar().ToString());

        komut.Dispose();
        return (sonuc >= 1);
    }

     public static bool VeriGuncelle(string kimlik, string veri_sütunu, string yeni_veri, MySqlConnection açık_bağlantı)
    {
        try
        {
            //Burdaki tablo ismi ayarlar dosyasından çekilecektir.
            string tablo = "kullanıcı";

            string kod = $"Update {tablo} SET {veri_sütunu} = @yeni_veri WHERE Kullanıcı = @kimlik";

            MySqlCommand komut = new MySqlCommand(kod, açık_bağlantı);

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

    public static bool kullanıcıEkle(Models.Kullanıcı kullanıcı)
    {
        string cs = Bağlantı.bağlantı_dizisi;

        MySqlConnection bağlantı = new MySqlConnection(cs);
        bağlantı.Open();

        string tablo_ismi = "kullanıcı";
        
        if(!VeriVarAçık("Kullanıcı_Adı", kullanıcı.Adı, bağlantı) && !VeriVarAçık("E_Posta", kullanıcı.E_posta, bağlantı))
        {
            kullanıcı.Şifre = Parolalar.KarılmışParola(kullanıcı.Şifre, Parolalar.Tuz());

            string kod = 
            $"INSERT INTO {tablo_ismi}(Kullanıcı_Adı, E_posta, Parola, Tür, Kimlik) VALUES(@kullanıcıadı, @E_Posta, @Parola, @Tür, @Kimlik);";

            MySqlCommand komut = new MySqlCommand(kod, bağlantı);

            komut.Parameters.AddWithValue("@kullanıcıadı", kullanıcı.Adı);
            komut.Parameters.AddWithValue("@E_Posta", kullanıcı.E_posta);
            komut.Parameters.AddWithValue("@Parola", kullanıcı.Şifre);
            komut.Parameters.AddWithValue("@Tür", kullanıcı.Tür.ToString());
            komut.Parameters.AddWithValue("@Kimlik", kullanıcı.Kimlik);

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

    public static Models.Kullanıcı kullanıcıAl_Kimlik_Açık(string Kimlik, MySqlConnection açık_bağlantı)
    {
        Kullanıcı_tür tür;

        string tablo = "kullanıcı";

        string kod = $"SELECT COUNT(kimlik) from {tablo} where kimlik = @veri";

        MySqlCommand komut = new MySqlCommand(kod, açık_bağlantı);

        komut.Parameters.AddWithValue("@veri", Kimlik);

        int sonuc = int.Parse(komut.ExecuteScalar().ToString());

        if(sonuc < 1)
        {
            komut.Dispose();
            return null;
        }
        
        komut.Dispose();

        kod = $"SELECT * FROM kullanıcı WHERE kimlik = @kullanıcı_kimliği";

        komut = new MySqlCommand(kod, açık_bağlantı);

        komut.Parameters.AddWithValue("@kullanıcı_kimliği",Kimlik);
        Models.Kullanıcı kullanıcı = new Models.Kullanıcı();

        MySqlDataReader okuyucu =  komut.ExecuteReader();

        while(okuyucu.Read())
        {
            kullanıcı.Adı = okuyucu["Kullanıcı_Adı"].ToString();
            kullanıcı.E_posta = okuyucu["E_posta"].ToString();
            kullanıcı.Şifre = okuyucu["Parola"].ToString();
            Enum.TryParse<Kullanıcı_tür>(okuyucu["Tür"].ToString(), out tür);
            kullanıcı.Tür = tür;
            kullanıcı.Kimlik = okuyucu["Kimlik"].ToString();
        }
        
        komut.Dispose();
        return kullanıcı;
    }

    public static Models.Kullanıcı kullanıcıAl_KullanıcıAdı_Açık(string Kullanıcı_Adı, MySqlConnection açık_bağlantı)
    {
        Kullanıcı_tür tür;

        string tablo = "kullanıcı";

        string kod = $"SELECT COUNT(kullanıcı_adı) from {tablo} where kullanıcı_adı = @veri";

        MySqlCommand komut = new MySqlCommand(kod, açık_bağlantı);

        komut.Parameters.AddWithValue("@veri", Kullanıcı_Adı);

        int sonuc = int.Parse(komut.ExecuteScalar().ToString());

        if(sonuc < 1)
        {   
            komut.Dispose();
            return null;
        }

        kod = $"SELECT * FROM kullanıcı WHERE Kullanıcı_Adı = @Kullanıcı_Adı";

        komut.Dispose();

        komut = new MySqlCommand(kod, açık_bağlantı);

        komut.Parameters.AddWithValue("@Kullanıcı_Adı",Kullanıcı_Adı);
        Models.Kullanıcı kullanıcı = new Models.Kullanıcı();

        MySqlDataReader okuyucu =  komut.ExecuteReader();

        while(okuyucu.Read())
        {
            kullanıcı.Adı = okuyucu["Kullanıcı_Adı"].ToString();
            kullanıcı.E_posta = okuyucu["E_posta"].ToString();
            kullanıcı.Şifre = okuyucu["Parola"].ToString();
            Enum.TryParse<Kullanıcı_tür>(okuyucu["Tür"].ToString(), out tür);
            kullanıcı.Tür = tür;
            kullanıcı.Kimlik = okuyucu["Kimlik"].ToString();
        }
        
        komut.Dispose();

        return kullanıcı;
    }
    
    public static bool KullanıcıSil(string kimlik)
    {
        string[] tablo_isimleri = new string[4];
        tablo_isimleri[0] = "kullanıcı";
        tablo_isimleri[1] = "bilgi_doğrulama";
        tablo_isimleri[2] = "oturum";
        tablo_isimleri[3] = "favoriler";
        try
        {
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizisi);
            bağlantı.Open();

            string kod = $"DELETE FROM {tablo_isimleri[0]} WHERE kimlik = @kimlik;";

            MySqlCommand komut = new MySqlCommand(kod,bağlantı);
            komut.Parameters.AddWithValue("@kimlik",kimlik);

            komut.ExecuteNonQuery();

            komut.Dispose();

            for(int i = 1; i<3; i++)
            {
                kod = $"DELETE FROM {tablo_isimleri[i]} WHERE kullanıcı = @kimlik;";

                komut = new MySqlCommand(kod,bağlantı);
                komut.Parameters.AddWithValue("@kimlik",kimlik);

                komut.ExecuteNonQuery();

                komut.Dispose();
            }

            FavorilerFonksiyonları.VeriGuncelle(kimlik, "Kullanıcı","anonim");

            bağlantı.Close();
            bağlantı.Dispose();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static bool KullanıcıAdıDeğiştir(string GirilenParola, string Kimlik, string Yeni_KullanıcıAdı)
    {
        try
        {
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizisi);
            bağlantı.Open();

            string kod = $"Update {TabloAdı} SET kullanıcı_adı = @yeni_veri WHERE Kimlik = @kimlik";

            MySqlCommand komut = new MySqlCommand(kod,bağlantı);
            komut.Parameters.AddWithValue("@kimlik",Kimlik);
            komut.Parameters.AddWithValue("@yeni_veri",Yeni_KullanıcıAdı);

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

    public static string Yeni_Kimlik()
    {
        RandomNumberGenerator üreteç =RandomNumberGenerator.Create();
        byte[] köri = new byte[24];

        üreteç.GetBytes(köri);
        string kimlik = Convert.ToBase64String(köri);
        if(!VeriVar("kimlik", kimlik))
        {
            return kimlik;
        }
        else
        {
            return Yeni_Kimlik();
        }
    }
}