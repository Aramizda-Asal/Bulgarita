using System.Reflection.Metadata;

namespace bulgarita.Models;

public class Roller
{
    public static readonly string[] RollerDizisi = ["Nokta Ekleyici",
        "Nokta Düzenleyici","Nokta Silici","Rol Atayıcı/Alıcı","Kullanıcı Silici"];
    public enum RollerE
    {
        NoktaEkleyici,
        NoktaDüzenleyici,
        NoktaSilici,
        RolAtayıcıAlıcı,
        KullanıcıSilici
    }
    public string Kullanıcı;
    public string Rol;
    public Roller(string Kullanıcı, string Rol)
    {
        this.Kullanıcı = Kullanıcı;
        this.Rol = Rol;
    }
}