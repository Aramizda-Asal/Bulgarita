// Copyright (C) 2025 Güneş Balcı, Habil Tataroğulları, Yusuf Kozan

using System;
using System.Text;
using bulgarita.Models;
using MySql.Data.MySqlClient;
using EmailValidation;

namespace bulgarita.Services;

public static class OturumFonksiyonları
{
    public static Models.Oturum OturumBaşlat(string kullanıcı_adı, string parola)
    {
        MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizisi);
        bağlantı.Open();

        if (KullanıcıFonksiyonları.GirişBilgileriDoğru(kullanıcı_adı, parola, bağlantı))
        {
            Kullanıcı kullanıcı;
            if(EmailValidator.Validate(kullanıcı_adı, true, true))
            {
                kullanıcı = KullanıcıFonksiyonları.kullanıcıAl_Eposta_Açık(kullanıcı_adı, bağlantı);
            }
            else
            {
                kullanıcı = KullanıcıFonksiyonları.kullanıcıAl_KullanıcıAdı_Açık(kullanıcı_adı, bağlantı);
            }

            Oturum yeni_oturum = new Oturum(kullanıcı, 8);
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