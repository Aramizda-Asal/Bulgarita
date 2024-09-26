using System.Collections;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using bulgarita.Services;

namespace bulgarita.Models;

public class Oturum
{
    public string Kullanıcı;
    public string Kimlik;
    public DateTime Başlangıç;
    public DateTime Bitiş;

    public Oturum() {}
    public Oturum(string kullanıcı_kimliği, double süre_saat)
    {
        Kullanıcı = kullanıcı_kimliği;
        Kimlik = YeniKimlik();
        Başlangıç = DateTime.Now;
        Bitiş = Başlangıç.AddHours(süre_saat);
    }
    public override string ToString()
    {
        string ayraç = "üğ₺";
        StringBuilder çıktı = new StringBuilder();
        çıktı.Append(Kullanıcı);
        çıktı.Append(ayraç);
        çıktı.Append(Kimlik);
        çıktı.Append(ayraç);
        çıktı.Append(Başlangıç.ToString("yyyyMMddHHmmss"));
        çıktı.Append(ayraç);
        çıktı.Append(Bitiş.ToString("yyyyMMddHHmmss"));
        return çıktı.ToString();
    }
    public virtual string[] ToStringArray()
    {
        string[] çıktı = new string[4];
        çıktı[0] = Kullanıcı;
        çıktı[1] = Kimlik;
        çıktı[2] = Başlangıç.ToString("yyyyMMddHHmmss");
        çıktı[3] = Bitiş.ToString("yyyyMMddHHmmss");
        return çıktı;
    }

    public static string YeniKimlik()
    {
        RandomNumberGenerator üreteç = RandomNumberGenerator.Create();
        byte[] rastgele = new byte[36];
        üreteç.GetBytes(rastgele);
        string sonuç = WebUtility.UrlEncode(Convert.ToBase64String(rastgele));
        üreteç.Dispose();

        if (OturumVT.KimlikVar(sonuç))
        {
            return YeniKimlik();
        }
        else
        {
            return sonuç;
        }
    }
}