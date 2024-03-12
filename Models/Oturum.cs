using System.Collections;
using System.Security.Cryptography;
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

    public static string YeniKimlik()
    {
        RandomNumberGenerator üreteç = RandomNumberGenerator.Create();
        byte[] rastgele = new byte[36];
        üreteç.GetBytes(rastgele);
        string sonuç = Convert.ToBase64String(rastgele);
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