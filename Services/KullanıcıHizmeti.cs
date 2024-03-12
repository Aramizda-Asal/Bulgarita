using bulgarita.Models;
using bulgarita;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;
using EmailValidation;

namespace bulgarita.Services;

public static class KullanıcıFonksiyonları
{
    public static bool BilgiDoğru(string kimlik, string veri, string veri_sütunu, MySqlConnection açık_bağlantı)
    {
        string kod = $"SELECT COUNT(kimlik) FROM {Bağlantı.Kullanıcı_Tablosu} WHERE {veri_sütunu} = @veri AND kimlik = @kimlik";

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

        string kod = $"SELECT COUNT({sütun}) FROM {Bağlantı.Kullanıcı_Tablosu} WHERE {sütun} = @veri";

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
        string kod = $"SELECT COUNT({sütun}) FROM {Bağlantı.Kullanıcı_Tablosu} WHERE {sütun} = @veri";

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
            string kod = $"Update {Bağlantı.Kullanıcı_Tablosu} SET {veri_sütunu} = @yeni_veri WHERE Kullanıcı = @kimlik";

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

        if(!VeriVarAçık("Kullanıcı_Adı", kullanıcı.Adı, bağlantı) && !VeriVarAçık("E_Posta", kullanıcı.E_posta, bağlantı))
        {
            kullanıcı.Şifre = Parolalar.KarılmışParola(kullanıcı.Şifre, Parolalar.Tuz());

            string kod = 
            $"INSERT INTO {Bağlantı.Kullanıcı_Tablosu}(Kullanıcı_Adı, E_posta, Parola, Tür, Kimlik) VALUES(@kullanıcıadı, @E_Posta, @Parola, @Tür, @Kimlik);";

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

        string kod = $"SELECT COUNT(kimlik) from {Bağlantı.Kullanıcı_Tablosu} where kimlik = @veri";

        MySqlCommand komut = new MySqlCommand(kod, açık_bağlantı);

        komut.Parameters.AddWithValue("@veri", Kimlik);

        int sonuc = int.Parse(komut.ExecuteScalar().ToString());

        if(sonuc < 1)
        {
            komut.Dispose();
            return null;
        }
        
        komut.Dispose();

        kod = $"SELECT * FROM {Bağlantı.Kullanıcı_Tablosu} WHERE kimlik = @kullanıcı_kimliği";

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
        
        okuyucu.Close();
        
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

        okuyucu.Close();
        
        komut.Dispose();

        return kullanıcı;
    }
    
    public static bool KullanıcıSil(string kimlik)
    {
        try
        {
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizisi);
            bağlantı.Open();

            string kod = $"DELETE FROM {Bağlantı.Kullanıcı_Tablosu} WHERE kimlik = @kimlik;";

            MySqlCommand komut = new MySqlCommand(kod,bağlantı);
            komut.Parameters.AddWithValue("@kimlik",kimlik);

            komut.ExecuteNonQuery();

            komut.Dispose();

            //Diğer tablolardan silinmesi diğer hizmetler eklendiğinde eklenecektir.

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
        MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizisi);
        bağlantı.Open();

        Models.Kullanıcı kullanıcı = kullanıcıAl_Kimlik_Açık(Kimlik,bağlantı);
        bool çıktı = false;

        if(Parolalar.ParolaDoğru(GirilenParola,kullanıcı.Şifre))
        {
            try
            {
                string kod = $"Update {Bağlantı.Kullanıcı_Tablosu} SET kullanıcı_adı = @yeni_veri WHERE Kimlik = @kimlik";

                MySqlCommand komut = new MySqlCommand(kod,bağlantı);
                komut.Parameters.AddWithValue("@kimlik",Kimlik);
                komut.Parameters.AddWithValue("@yeni_veri",Yeni_KullanıcıAdı);

                komut.ExecuteNonQuery();
                komut.Dispose();

                çıktı = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                çıktı = false;
            }
        }

        bağlantı.Close();
        bağlantı.Dispose();
        return çıktı;
    }

    public static bool ParolaDeğiştir(string GirilenParola, string Kimlik, string Yeni_Parola)
    {
        MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizisi);
        bağlantı.Open();

        Models.Kullanıcı kullanıcı = kullanıcıAl_Kimlik_Açık(Kimlik,bağlantı);
        bool çıktı = false;

        Yeni_Parola = Parolalar.KarılmışParola(Yeni_Parola,Parolalar.Tuz());

        if(Parolalar.ParolaDoğru(GirilenParola,kullanıcı.Şifre))
        {
            try
            {
                string kod = $"Update {Bağlantı.Kullanıcı_Tablosu} SET Parola = @yeni_veri WHERE Kimlik = @kimlik";

                MySqlCommand komut = new MySqlCommand(kod,bağlantı);
                komut.Parameters.AddWithValue("@kimlik",Kimlik);
                komut.Parameters.AddWithValue("@yeni_veri",Yeni_Parola);

                komut.ExecuteNonQuery();
                komut.Dispose();

                çıktı = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                çıktı = false;
            }
        }

        bağlantı.Close();
        bağlantı.Dispose();
        return çıktı;
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

    public static bool Kullanıcı_Girişi(string GirilenParola, string GirilenKullanıcıAdı)
    {
        MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizisi);
        bağlantı.Open();
        bool çıktı = false;

        if(EmailValidator.Validate(GirilenKullanıcıAdı, true, true))
        {
            if(VeriVarAçık("E_Posta", GirilenKullanıcıAdı, bağlantı))
            {
                string kod = $"SELECT Parola FROM {Bağlantı.Kullanıcı_Tablosu} WHERE E_Posta = @e_posta";

                MySqlCommand komut = new MySqlCommand(kod, bağlantı);
                komut.Parameters.AddWithValue("@e_posta", GirilenKullanıcıAdı);

                if(Parolalar.ParolaDoğru(GirilenParola, komut.ExecuteScalar().ToString()))
                {
                    çıktı = true;
                }
                komut.Dispose();
            }
        }
        else
        {
            if(VeriVarAçık("Kullanıcı_Adı", GirilenKullanıcıAdı, bağlantı))
            {
                Kullanıcı kullanıcı = kullanıcıAl_KullanıcıAdı_Açık(GirilenKullanıcıAdı, bağlantı);
                if(Parolalar.ParolaDoğru(GirilenParola, kullanıcı.Şifre))
                {
                    çıktı = true;
                }
            }
        }
        



        bağlantı.Close();
        bağlantı.Dispose();
        return çıktı;
    }
}