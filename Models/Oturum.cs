using System.Security.Cryptography;
using bulgarita.Services;

namespace bulgarita.Models;

public class Oturum
{
    public string Kullanıcı;
    public string Kimlik;
    public DateTime Başlangıç;
    public DateTime Bitiş;

    public static string YeniKimlik()
    {
        RandomNumberGenerator üreteç = RandomNumberGenerator.Create();
        byte[] rastgele = new byte[36];
        üreteç.GetBytes(rastgele);
        string sonuç = Convert.ToBase64String(rastgele);
        üreteç.Dispose();

        if (OturumFonksiyonları.KimlikVar(sonuç))
        {
            return YeniKimlik();
        }
        else
        {
            return sonuç;
        }
    }
}