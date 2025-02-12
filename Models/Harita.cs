using System.Security.Cryptography;
using System.Text;
using bulgarita.Services;

namespace bulgarita.Models;

public class Harita
{
    public double EnlemDrc;
    public double BoylamDrc;
    public string Bulgarca_Latin_İsim;
    public string Bulgarca_Kiril_İsim;
    public string Türkçe_İsim;
    public string Osmanlıca_İsim;
    public string Bölge_Türü;
    public string Üst_Bölge;
    public string Kimlik;

    public Harita() {}
    public Harita(double EnlemDrc, double BoylamDrc, string Bulgarca_Latin_İsim, string Bulgarca_Kiril_İsim,
    string Türkçe_İsim, string Osmanlıca_İsim, string Bölge_Türü, string Üst_Bölge, string Kimlik)
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
        ];
        return çıktı;
    }
}