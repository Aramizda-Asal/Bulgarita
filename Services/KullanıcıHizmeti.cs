using bulgarita.Models;
using bulgarita;
using MySql.Data.MySqlClient;

namespace bulgarita.Services;

public static class KullanıcıFonksiyonları
{
    private static string TabloAdı = "kullanıcı";
    //Buradaki tablo ismi ayarlar dosyasından çekilecektir.

    public static bool BilgiDoğru(string kimlik, string veri, string veri_sütunu)
    {
        string cs = Bağlantı.bağlantı_dizisi;
        MySqlConnection bağlantı = new MySqlConnection(cs);
        bağlantı.Open();

        //Burdaki tablo ismi ayarlar dosyasından çekilecektir.
        string tablo = "kullanıcı";

        string kod = $"SELECT COUNT(kimlik) FROM {tablo} WHERE {veri_sütunu} = @veri AND kimlik = @kimlik";

        MySqlCommand komut = new MySqlCommand(kod, bağlantı);

        komut.Parameters.AddWithValue("@veri", veri);
        komut.Parameters.AddWithValue("@kimlik", kimlik);

        int sonuc = int.Parse(komut.ExecuteScalar().ToString());

        komut.Dispose();
        bağlantı.Close();
        bağlantı.Dispose();

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

    public static bool VeriGuncelle(string kimlik, string veri_sütunu, string eski_veri, string yeni_veri)
    {
        if(BilgiDoğru(kimlik, eski_veri, veri_sütunu))
        {
            string cs = Bağlantı.bağlantı_dizisi;
            
            MySqlConnection bağlantı = new MySqlConnection(cs);
            bağlantı.Open();

            //Burdaki tablo ismi ayarlar dosyasından çekilecektir.
            string tablo = "kullanıcı";

            string kod = $"Update {tablo} SET {veri_sütunu} = @yeni_veri WHERE Kimlik = @kimlik";

            MySqlCommand komut = new MySqlCommand(kod, bağlantı);

            komut.Parameters.AddWithValue("@yeni_veri", yeni_veri);
            komut.Parameters.AddWithValue("@kimlik", kimlik);

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

    public static bool kullanıcıEkle(Models.Kullanıcı kullanıcı)
    {
        string cs = Bağlantı.bağlantı_dizisi;

        MySqlConnection bağlantı = new MySqlConnection(cs);
        bağlantı.Open();

        string tablo_ismi = "kullanıcı";
        
        if(!VeriVar("Kullanıcı_Adı", kullanıcı.Adı) && !VeriVar("E_Posta", kullanıcı.E_posta))
        {
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

    public static Models.Kullanıcı kullanıcıAl_Kimlik(string Kimlik)
    {
        string cs = Bağlantı.bağlantı_dizisi;

        Kullanıcı_tür tür;

        string tablo = "kullanıcı";

        MySqlConnection bağlantı = new MySqlConnection(cs);
        bağlantı.Open();

        string kod = $"SELECT COUNT(kimlik) from {tablo} where kimlik = @veri";

        MySqlCommand komut = new MySqlCommand(kod, bağlantı);

        komut.Parameters.AddWithValue("@veri", Kimlik);

        int sonuc = int.Parse(komut.ExecuteScalar().ToString());

        if(sonuc < 1)
        {
            komut.Dispose();
            bağlantı.Close();
            bağlantı.Dispose();
            return null;
        }
        
        komut.Dispose();

        kod = $"SELECT * FROM kullanıcı WHERE kimlik = @kullanıcı_kimliği";

        komut = new MySqlCommand(kod, bağlantı);

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
        bağlantı.Close();
        bağlantı.Dispose();

        return kullanıcı;
    }

    public static Models.Kullanıcı kullanıcıAl_KullanıcıAdı(string Kullanıcı_Adı)
    {
        string cs = Bağlantı.bağlantı_dizisi;

        Kullanıcı_tür tür;

        MySqlConnection bağlantı = new MySqlConnection(cs);
        bağlantı.Open();

        string tablo = "kullanıcı";

        string kod = $"SELECT COUNT(kullanıcı_adı) from {tablo} where kullanıcı_adı = @veri";

        MySqlCommand komut = new MySqlCommand(kod, bağlantı);

        komut.Parameters.AddWithValue("@veri", Kullanıcı_Adı);

        int sonuc = int.Parse(komut.ExecuteScalar().ToString());

        if(sonuc < 1)
        {   
            komut.Dispose();
            bağlantı.Close();
            bağlantı.Dispose();
            return null;
        }

        kod = $"SELECT * FROM kullanıcı WHERE Kullanıcı_Adı = @Kullanıcı_Adı";

        komut.Dispose();

        komut = new MySqlCommand(kod, bağlantı);

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
        bağlantı.Close();
        bağlantı.Dispose();

        return kullanıcı;
        
    }
    public static bool KullanıcıSil(string kimlik)
    {
        try
        {
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizisi);
            bağlantı.Open();

            string kod = $"DELETE FROM {TabloAdı} WHERE kimlik = @kimlik;";

            MySqlCommand komut = new MySqlCommand(kod,bağlantı);
            komut.Parameters.AddWithValue("@kimlik",kimlik);

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
}