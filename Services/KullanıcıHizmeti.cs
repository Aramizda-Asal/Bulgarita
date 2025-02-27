// Copyright (C) 2025 Güneş Balcı, Habil Tataroğulları, Yusuf Kozan

using bulgarita.Models;
using bulgarita;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;
using EmailValidation;
using System.Net;
using System.Collections;

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
        if (!EmailValidator.Validate(kullanıcı.E_posta, true, true))
        {
            return false;
        }

        string cs = Bağlantı.bağlantı_dizisi;

        MySqlConnection bağlantı = new MySqlConnection(cs);
        bağlantı.Open();

        if(!VeriVarAçık("Kullanıcı_Adı", kullanıcı.Adı, bağlantı) && !VeriVarAçık("E_Posta", kullanıcı.E_posta, bağlantı))
        {
            kullanıcı.Şifre = Parolalar.KarılmışParola(kullanıcı.Şifre, Parolalar.Tuz());

            string kod = 
            $"INSERT INTO {Bağlantı.Kullanıcı_Tablosu}(Kullanıcı_Adı, E_posta, Parola, Kimlik) VALUES(@kullanıcıadı, @E_Posta, @Parola, @Kimlik);";

            MySqlCommand komut = new MySqlCommand(kod, bağlantı);

            komut.Parameters.AddWithValue("@kullanıcıadı", kullanıcı.Adı);
            komut.Parameters.AddWithValue("@E_Posta", kullanıcı.E_posta);
            komut.Parameters.AddWithValue("@Parola", kullanıcı.Şifre);
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
        MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizisi);
        bağlantı.Open();
        Models.Kullanıcı sonuç = kullanıcıAl_Kimlik_Açık(Kimlik, bağlantı);
        bağlantı.Close(); bağlantı.Dispose();

        return sonuç;
    }
    public static Models.Kullanıcı kullanıcıAl_Kimlik_Açık(string Kimlik, MySqlConnection açık_bağlantı)
    {
        string kod = $"SELECT COUNT(Kimlik) FROM {Bağlantı.Kullanıcı_Tablosu} WHERE Kimlik = @kimlik;";
        MySqlCommand komut = new MySqlCommand(kod, açık_bağlantı);
        komut.Parameters.AddWithValue("@kimlik", Kimlik);

        int sonuc = int.Parse(komut.ExecuteScalar().ToString());
        komut.Dispose();

        if(sonuc < 1)
        {
            return null;
        }

        kod = $"SELECT * FROM {Bağlantı.Kullanıcı_Tablosu} WHERE kimlik = @kullanıcı_kimliği";
        komut = new MySqlCommand(kod, açık_bağlantı);
        komut.Parameters.AddWithValue("@kullanıcı_kimliği",Kimlik);

        Models.Kullanıcı kullanıcı = null;

        MySqlDataReader okuyucu =  komut.ExecuteReader();
        if (okuyucu.HasRows)
        {
            kullanıcı = new Models.Kullanıcı();
            while(okuyucu.Read())
            {
                kullanıcı.Adı = okuyucu["Kullanıcı_Adı"].ToString();
                kullanıcı.E_posta = okuyucu["E_posta"].ToString();
                kullanıcı.Şifre = okuyucu["Parola"].ToString();
                kullanıcı.Kimlik = okuyucu["Kimlik"].ToString();
                break;
            }
        }
        
        okuyucu.Close();        
        komut.Dispose();

        return kullanıcı;
    }

    public static Models.Kullanıcı kullanıcıAl_KullanıcıAdı(string Kullanıcı_Adı)
    {
        MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizisi);
        bağlantı.Open();
        Models.Kullanıcı sonuç = kullanıcıAl_KullanıcıAdı_Açık(Kullanıcı_Adı, bağlantı);
        bağlantı.Close(); bağlantı.Dispose();

        return sonuç;
    }
    public static Models.Kullanıcı kullanıcıAl_KullanıcıAdı_Açık(string Kullanıcı_Adı, MySqlConnection açık_bağlantı)
    {
        string kod = $"SELECT COUNT(kullanıcı_adı) from {Bağlantı.Kullanıcı_Tablosu} where kullanıcı_adı = @veri";

        MySqlCommand komut = new MySqlCommand(kod, açık_bağlantı);

        komut.Parameters.AddWithValue("@veri", Kullanıcı_Adı);

        int sonuc = int.Parse(komut.ExecuteScalar().ToString());

        if(sonuc < 1)
        {   
            komut.Dispose();
            return null;
        }

        kod = $"SELECT * FROM {Bağlantı.Kullanıcı_Tablosu} WHERE Kullanıcı_Adı = @Kullanıcı_Adı";

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

            bağlantı.Close();
            bağlantı.Dispose();
            return true;
        }
        catch
        {
            return false;
        }
    }

    /**
    * <summary>
    * Kullanıcının kullanıcı adını değiştirebilmesini sağlar.
    * </summary>
    * <remarks>
    * <para>
    * Kullanıcının girdiği parola doğruysa kullanıcı adı değiştirilir.
    * </para>
    * </remarks>
    *
    * <param name="KullanıcıKimliği">Adı değiştirilecek kullanıcının kimliği</param>
    * <param name="GirilenParola">Kullanıcının karılmamış parolası</param>
    * <param name="YeniKullanıcıAdı">Kullanıcının yeni kullanıcı adı</param>
    *
    * <returns>
    * Kullanıcı adı başarıyla değiştirilirse <c>true</c>,
    * değiştirilmezse <c>false</c>.
    * </returns>
    *
    * <seealso cref="bulgarita.Controllers.Kullanıcı.KullanıcıAdıDeğiştir(string, string, string)"/>
    */
    public static bool KullanıcıAdıDeğiştir(string KullanıcıKimliği,
                                            string GirilenParola,
                                            string YeniKullanıcıAdı)
    {
        MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizisi);
        bağlantı.Open();

        Models.Kullanıcı kullanıcı = kullanıcıAl_Kimlik_Açık(KullanıcıKimliği,
                                         bağlantı);
        
        if (kullanıcı == null)
        {
            return false;
        }

        bool çıktı = false;

        if(Parolalar.ParolaDoğru(GirilenParola, kullanıcı.Şifre))
        {
            try
            {
                string kod = $"Update {Bağlantı.Kullanıcı_Tablosu} " +
                             "SET Kullanıcı_Adı = @yeni_veri " +
                             "WHERE Kimlik = @kimlik;";

                MySqlCommand komut = new MySqlCommand(kod,bağlantı);
                komut.Parameters.AddWithValue("@kimlik", KullanıcıKimliği);
                komut.Parameters.AddWithValue("@yeni_veri", YeniKullanıcıAdı);

                komut.ExecuteNonQuery();
                komut.Dispose();

                çıktı = true;
            }
            catch
            {
                çıktı = false;
            }
        }

        bağlantı.Close();
        bağlantı.Dispose();
        return çıktı;
    }

    /**
    * <summary>
    * Kullanıcının parolasını değiştirebilmesini sağlar.
    * </summary>
    * <remarks>
    * <para>
    * Kullanıcının girdiği mevcut parola doğruysa
    * yeni parola karılıp veri tabanına kaydedilir.
    * </para>
    * <para>
    * <see cref="bulgarita.Controllers.Kullanıcı.ParolaDeğiştir(string, string, string)">API Denetçisi</see>
    * üzerinden kullanılması için tasarlanmıştır.
    * </para>
    * </remarks>
    * 
    * <param name="KullanıcıKimliği">Parolası değiştirilecek kullanıcının kimliği</param>
    * <param name="MevcutParola">Kullanıcının mevcut parolasının karılmamış hâli</param>
    * <param name="YeniParola">Kullanıcının yeni parolasının karılmamış hâli</param>
    *
    * <returns>
    * Parola başarıyla değiştirilirse <c>true</c>,
    * değiştirilmezse <c>false</c>.
    * </returns>
    *
    * <seealso cref="bulgarita.Controllers.Kullanıcı.ParolaDeğiştir(string, string, string)"/>
    */
    public static bool ParolaDeğiştir(string KullanıcıKimliği,
                                      string MevcutParola, string YeniParola)
    {
        MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizisi);
        bağlantı.Open();

        Models.Kullanıcı kullanıcı = kullanıcıAl_Kimlik_Açık(KullanıcıKimliği, bağlantı);
        
        if (kullanıcı == null)
        {
            return false;
        }

        bool çıktı = false;

        if(Parolalar.ParolaDoğru(MevcutParola, kullanıcı.Şifre))
        {
            try
            {
                YeniParola = Parolalar.KarılmışParola(YeniParola, Parolalar.Tuz());
                string kod = $"UPDATE {Bağlantı.Kullanıcı_Tablosu} " +
                            "SET Parola = @yeni_veri WHERE Kimlik = @kimlik;";

                MySqlCommand komut = new MySqlCommand(kod, bağlantı);
                komut.Parameters.AddWithValue("@kimlik", KullanıcıKimliği);
                komut.Parameters.AddWithValue("@yeni_veri", YeniParola);

                komut.ExecuteNonQuery();
                komut.Dispose();

                çıktı = true;
            }
            catch
            {
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

    public static bool GirişBilgileriDoğru(string kullanıcı_adı, string parola)
    {
        MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizisi);
        bağlantı.Open();
        bool çıktı = false;

        if(EmailValidator.Validate(kullanıcı_adı, true, true))
        {
            // E-posta ile giriş yapılıyorsa
            if(VeriVarAçık("E_Posta", kullanıcı_adı, bağlantı))
            {
                string kod = $"SELECT Parola FROM {Bağlantı.Kullanıcı_Tablosu} WHERE E_Posta = @e_posta;";

                MySqlCommand komut = new MySqlCommand(kod, bağlantı);
                komut.Parameters.AddWithValue("@e_posta", kullanıcı_adı);

                if(Parolalar.ParolaDoğru(parola, komut.ExecuteScalar().ToString()))
                {
                    çıktı = true;
                }
                komut.Dispose();
            }
        }
        else
        {
            // Kullanıcı adı ile giriş yapılıyorsa
            if(VeriVarAçık("Kullanıcı_Adı", kullanıcı_adı, bağlantı))
            {
                Kullanıcı kullanıcı = kullanıcıAl_KullanıcıAdı_Açık(kullanıcı_adı, bağlantı);
                if(Parolalar.ParolaDoğru(parola, kullanıcı.Şifre))
                {
                    çıktı = true;
                }
            }
        }
        
        bağlantı.Close();
        bağlantı.Dispose();
        return çıktı;
    }
    public static bool GirişBilgileriDoğru(string kullanıcı_adı, string parola,
                        MySqlConnection açık_bağlantı)
    {
        bool çıktı = false;

        if(EmailValidator.Validate(kullanıcı_adı, true, true))
        {
            // E-posta ile giriş yapılıyorsa
            if(VeriVarAçık("E_Posta", kullanıcı_adı, açık_bağlantı))
            {
                string kod = $"SELECT Parola FROM {Bağlantı.Kullanıcı_Tablosu} WHERE E_Posta = @e_posta;";

                MySqlCommand komut = new MySqlCommand(kod, açık_bağlantı);
                komut.Parameters.AddWithValue("@e_posta", kullanıcı_adı);

                if(Parolalar.ParolaDoğru(parola, komut.ExecuteScalar().ToString()))
                {
                    çıktı = true;
                }
                komut.Dispose();
            }
        }
        else
        {
            // Kullanıcı adı ile giriş yapılıyorsa
            if(VeriVarAçık("Kullanıcı_Adı", kullanıcı_adı, açık_bağlantı))
            {
                Kullanıcı kullanıcı = kullanıcıAl_KullanıcıAdı_Açık(kullanıcı_adı, açık_bağlantı);
                if(Parolalar.ParolaDoğru(parola, kullanıcı.Şifre))
                {
                    çıktı = true;
                }
            }
        }
        
        return çıktı;
    }

    public static List<string> İçerenKullanıcıAdlarıAl(string içerik)
    {
        MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizisi);
        bağlantı.Open();

        string kod_işlem1 = $"SELECT COUNT(Kullanıcı_Adı) FROM {Bağlantı.Kullanıcı_Tablosu}";
        kod_işlem1 += " WHERE Kullanıcı_Adı LIKE @içerik;";

        MySqlCommand komut = new MySqlCommand(kod_işlem1, bağlantı);
        string içerikString_tümü = "%" + içerik + "%";
        komut.Parameters.AddWithValue("@içerik", içerikString_tümü);

        int sonuc = int.Parse(komut.ExecuteScalar().ToString());
        komut.Dispose();

        if(sonuc < 1)
        {
            return null;
        }
        /*--------------------------------------------------------*/
        List<string> kullanıcılar = [];

        string kod_işlem2 = $"SELECT Kullanıcı_Adı, Kimlik FROM {Bağlantı.Kullanıcı_Tablosu}" +
        " WHERE Kullanıcı_Adı LIKE @içerik_aynı UNION DISTINCT " +

        $"SELECT Kullanıcı_Adı, Kimlik FROM {Bağlantı.Kullanıcı_Tablosu}" +
        " WHERE Kullanıcı_Adı LIKE @içerik_başta UNION DISTINCT "+

        $"SELECT Kullanıcı_Adı, Kimlik FROM {Bağlantı.Kullanıcı_Tablosu}" +
        " WHERE Kullanıcı_Adı LIKE @içerik_ortada UNION DISTINCT " +

        $"SELECT Kullanıcı_Adı, Kimlik FROM {Bağlantı.Kullanıcı_Tablosu}" +
        " WHERE Kullanıcı_Adı LIKE @içerik_sonda;";

        komut = new MySqlCommand(kod_işlem2, bağlantı);
        string içerikString_Aynı = içerik;
        string içerikString_Başta = içerik + "_%";
        string içerikString_Ortada = "%_" + içerik + "_%";
        string içerikString_Sonda = "%_" + içerik;
        komut.Parameters.AddWithValue("@içerik_aynı",içerikString_Aynı);
        komut.Parameters.AddWithValue("@içerik_başta",içerikString_Başta);
        komut.Parameters.AddWithValue("@içerik_ortada",içerikString_Ortada);
        komut.Parameters.AddWithValue("@içerik_sonda",içerikString_Sonda);

        MySqlDataReader okuyucu =  komut.ExecuteReader();
        if (okuyucu.HasRows)
        {
            while(okuyucu.Read())
            {
                string kullanıcıAdı = okuyucu["Kullanıcı_Adı"].ToString();
                kullanıcılar.Add(kullanıcıAdı);
            }
        }
        
        okuyucu.Close();        
        komut.Dispose();

        bağlantı.Close();
        bağlantı.Dispose();
      
        return kullanıcılar;
    }
}