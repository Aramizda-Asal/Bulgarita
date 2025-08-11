/*
 * Copyright (C) 2025 Güneş Balcı, Habil Tataroğulları, Yusuf Kozan
 *
 * This file is part of "Bulgaristan’da Harita Bazlı Yer İsimleri Uygulaması".
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Affero General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Affero General Public License for more details.
 * 
 * You should have received a copy of the GNU Affero General Public License
 * along with this program.  If not, see <https://www.gnu.org/licenses/>.
 * 
 */

using System.Security.Cryptography;
using System.Text;
using bulgarita.Services;
using Newtonsoft.Json;

namespace bulgarita.Models;

public class Harita
{
    [JsonProperty("enlem")]
    public double EnlemDrc;

    [JsonProperty("boylam")]
    public double BoylamDrc;

    [JsonProperty("Bulgarca_Latin")]
    public string Bulgarca_Latin_İsim;
    
    [JsonProperty("Bulgarca_Kiril")]
    public string Bulgarca_Kiril_İsim;
    
    [JsonProperty("Türkçe")]
    public string Türkçe_İsim;
    
    [JsonProperty("Osmanlıca")]
    public string Osmanlıca_İsim;
    
    [JsonProperty("bölge_türü")]
    public string Bölge_Türü;
    
    [JsonProperty("üst_bölge")]
    public string Üst_Bölge;
    
    [JsonProperty("kimlik")]
    public string Kimlik;

    [JsonProperty("aciklama")]
    public string Aciklama;

    [JsonIgnore]
    public static readonly string[][] UygunTürler = new string[][] {
        new string[] {"Ülke"},
        new string[] {"İl"},
        new string[] {"İlçe"},
        new string[] {"Kasaba", "Köy"}
    };

    public Harita() {}
    public Harita(double EnlemDrc, double BoylamDrc, string Bulgarca_Latin_İsim, string Bulgarca_Kiril_İsim,
    string Türkçe_İsim, string Osmanlıca_İsim, string Bölge_Türü, string Üst_Bölge, string Kimlik, string Aciklama)
    {
        this.EnlemDrc = EnlemDrc;
        this.BoylamDrc = BoylamDrc;
        this.Bulgarca_Latin_İsim = Bulgarca_Latin_İsim;
        this.Bulgarca_Kiril_İsim = Bulgarca_Kiril_İsim;
        this.Türkçe_İsim = Türkçe_İsim;
        this.Osmanlıca_İsim = Osmanlıca_İsim;
        this.Bölge_Türü = Bölge_Türü;
        this.Üst_Bölge = Üst_Bölge;
        this.Kimlik = Kimlik;
        this.Aciklama = Aciklama;
    }
    public Harita(string harita)
    {
        string[] haritaStringArray = harita.Split("/");
        this.EnlemDrc = Convert.ToDouble(haritaStringArray[0]);
        this.BoylamDrc = Convert.ToDouble(haritaStringArray[1]);
        this.Bulgarca_Latin_İsim = haritaStringArray[2];
        this.Bulgarca_Kiril_İsim = haritaStringArray[3];
        this.Türkçe_İsim = haritaStringArray[4];
        this.Osmanlıca_İsim = haritaStringArray[5];
        this.Bölge_Türü = haritaStringArray[6];
        this.Üst_Bölge = haritaStringArray[7];
        this.Kimlik = haritaStringArray[8];
        this.Aciklama = haritaStringArray[9];
    }

    /**
    * <summary>
    * Eşsiz bir kimlik ile yeni bir nokta nesnesi oluşturur.
    * </summary>
    */
    public static Harita YeniNokta(double EnlemDrc, double BoylamDrc,
            string BulgarcaLatinİsim, string BulgarcaKirilİsim,
            string Türkçeİsim, string Osmanlıcaİsim, string BölgeTürü,
            string ÜstBölge, string Aciklama)
    {
        // Ekvatordan 90 dereceden fazla uzaklaşılamaz.
        if (EnlemDrc > 90 || EnlemDrc < -90)
        {
            return null;
        }
        // Başlangıç meridyeninden 180 dereceden fazla uzaklaşılamaz.
        if (BoylamDrc > 180 || BoylamDrc < -180)
        {
            return null;
        }

        // Belirtilen üst bölge kayıtlı değilse o bilgi boş bırakılır.
        if (!HaritaFonksiyonları.KimlikKullanımda(ÜstBölge))
        {
            ÜstBölge = null;
        }

        // Parametreler uygunsa yeni bir kimlik üretilip nesne oluşturulur.
        string kimlik = YeniKimlik();
        Harita yeni_nokta = new Harita(
            EnlemDrc,
            BoylamDrc,
            BulgarcaLatinİsim,
            BulgarcaKirilİsim,
            Türkçeİsim,
            Osmanlıcaİsim,
            BölgeTürü,
            ÜstBölge,
            kimlik,
            Aciklama
        );

        return yeni_nokta;
    }

    /**
    * <summary>
    * Yeni bir eşsiz nokta kimliği üretir.
    * </summary>
    * <remarks>
    * <para>
    * 24 byte boyutunda kriptografik olarak güvenli bir rastgele sayı üretir.
    * Sonra sayıyı Base64 metnine dönüştürüp, kullanım durumunu denetler.
    * </para>
    * </remarks>
    *
    * <returns>
    * Üretilen rastgele Base64 metni kimlik olarak kullanımda değilse metni döndürür.
    * Metin kullanımdaysa kullanımda olmayan bir rastgele metin üretene kadar
    * kendisini çağırır.
    * </returns>
    */
    public static string YeniKimlik()
    {
        RandomNumberGenerator üreteç = RandomNumberGenerator.Create();
        byte[] rastgele = new byte[24];
        üreteç.GetBytes(rastgele);
        string sonuç = Convert.ToBase64String(rastgele);
        üreteç.Dispose();

        if (HaritaFonksiyonları.KimlikKullanımda(sonuç))
        {
            return YeniKimlik();
        }
        else
        {
            return sonuç;
        }
    }

    public string[] ToStringArray() 
    {
        string[] çıktı =
        [//köşeli parantez??
            this.EnlemDrc.ToString().Replace(',', '.'),
            this.BoylamDrc.ToString().Replace(',', '.'),
            this.Bulgarca_Latin_İsim,
            this.Bulgarca_Kiril_İsim,
            this.Türkçe_İsim,
            this.Osmanlıca_İsim,
            this.Bölge_Türü,
            this.Üst_Bölge,
            this.Kimlik,
            this.Aciklama,
        ];
        return çıktı;
    }
}