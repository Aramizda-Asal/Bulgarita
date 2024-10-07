using Newtonsoft.Json;

namespace bulgarita.Models;

public enum Kullanıcı_tür{kullanıcı, düzenleyici, allah}

public class Kullanıcı
{
    public string Kimlik;
    public string Adı;
    [JsonIgnore]
    public string Şifre;
    public string E_posta;
    public Kullanıcı_tür Tür;

    internal Kullanıcı() {}
    internal Kullanıcı(string Adı, string E_posta, string Şifre, Kullanıcı_tür Tür, string Kimlik)
    {
        this.Adı = Adı;
        this.E_posta = E_posta;
        this.Şifre = Şifre;
        this.Tür = Tür;
        this.Kimlik = Kimlik;
    }
}