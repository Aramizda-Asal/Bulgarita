namespace bulgarita.Models;

public enum Kullanıcı_tür{allah, düzenleyici, kullanıcı}

public class Kullanıcı
{
    public string Kimlik;
    public string Adı;
    public string Şifre;
    public string E_posta;
    public Kullanıcı_tür Tür;
}