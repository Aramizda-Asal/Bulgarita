using bulgarita.Models;
using bulgarita;
using MySql.Data.MySqlClient;

namespace bulgarita.Services;

public static class RollerFonksiyonları
{
    public static bool RolVer(Models.Roller roller)
    {
        string cs = Bağlantı.bağlantı_dizisi;
        
        MySqlConnection bağlantı = new MySqlConnection(cs);
        bağlantı.Open();

        if(!SatırVar_AçıkBağlantı(roller, bağlantı))
        {
            if(KullanıcıFonksiyonları.VeriVarAçık("Kimlik", roller.Kullanıcı, bağlantı))
            {
                string kod = $"INSERT INTO {Bağlantı.Roller_Tablosu} VALUES(@kullanıcı_kimliği, @rol);";

                MySqlCommand komut = new MySqlCommand(kod, bağlantı);

                komut.Parameters.AddWithValue("@kullanıcı_kimliği", roller.Kullanıcı);
                komut.Parameters.AddWithValue("@rol", roller.Rol);

                komut.ExecuteNonQuery();
                komut.Dispose();

                bağlantı.Close();
                bağlantı.Dispose();

                return true;
            }
        }
        return false;
    } 

    public static bool RolAl(Models.Roller roller)
    {
        string cs = Bağlantı.bağlantı_dizisi;
        
        MySqlConnection bağlantı = new MySqlConnection(cs);
        bağlantı.Open();

        if(SatırVar_AçıkBağlantı(roller, bağlantı))
        {
            string kod = $"DELETE FROM {Bağlantı.Roller_Tablosu} ";
            kod += $"WHERE Kullanıcı = @kullanıcı_kimliği AND Rol = @rol;";

            MySqlCommand komut = new MySqlCommand(kod, bağlantı);

            komut.Parameters.AddWithValue("@kullanıcı_kimliği", roller.Kullanıcı);
            komut.Parameters.AddWithValue("@rol", roller.Rol);

            komut.ExecuteNonQuery();
            komut.Dispose();

            bağlantı.Close();
            bağlantı.Dispose();

            return true;
        }
        return false;
    }

    /**
    * <summary>
    * Belirtilen kullanıcının nokta ekleme yetkisi olup olmadığını denetler.
    * </summary>
    *
    * <param name="kullanıcı">Rolleri incelenen kullanıcının nesnesi</param>
    *
    * <returns>
    * Kullanıcının Nokta Ekleyici rolü varsa <c>true</c>,
    * yoksa <c>false</c>.
    * </returns>
    */
    public static bool NoktaEkleyebilir(Kullanıcı kullanıcı)
    {
        Roller aranan = new Roller(kullanıcı.Kimlik, "Nokta Ekleyici");
        return SatırVar(aranan);
    }

    /**
    * <summary>
    * Belirtilen kullanıcının nokta düzenleme yetkisi olup olmadığını denetler.
    * </summary>
    *
    * <param name="kullanıcı">Rolleri incelenen kullanıcının nesnesi</param>
    *
    * <returns>
    * Kullanıcının Nokta Düzenleyici rolü varsa <c>true</c>,
    * yoksa <c>false</c>.
    * </returns>
    */
    public static bool NoktaDüzenleyebilir(Kullanıcı kullanıcı)
    {
        Roller aranan = new Roller(kullanıcı.Kimlik, "Nokta Düzenleyici");
        return SatırVar(aranan);
    }

    public static bool SatırVar_AçıkBağlantı(Models.Roller roller, MySqlConnection Açık_Bağlantı)
    {
        string kod = $"SELECT COUNT(Rol) FROM {Bağlantı.Roller_Tablosu} ";
        kod += $"WHERE Kullanıcı = @kullanıcı_kimliği AND Rol = @rol";

        MySqlCommand komut = new MySqlCommand(kod, Açık_Bağlantı);

        komut.Parameters.AddWithValue("@kullanıcı_kimliği", roller.Kullanıcı);
        komut.Parameters.AddWithValue("@rol", roller.Rol);

        int sonuc = int.Parse(komut.ExecuteScalar().ToString());

        komut.Dispose();
        return sonuc >= 1;
    }

    public static bool SatırVar(Models.Roller roller)
    {
        string cs = Bağlantı.bağlantı_dizisi;
        
        MySqlConnection bağlantı = new MySqlConnection(cs);
        bağlantı.Open();

        string kod = $"SELECT COUNT(Rol) FROM {Bağlantı.Roller_Tablosu} ";
        kod += $"WHERE Kullanıcı = @kullanıcı_kimliği AND Rol = @rol";

        MySqlCommand komut = new MySqlCommand(kod, bağlantı);

        komut.Parameters.AddWithValue("@kullanıcı_kimliği", roller.Kullanıcı);
        komut.Parameters.AddWithValue("@rol", roller.Rol);

        int sonuc = int.Parse(komut.ExecuteScalar().ToString());

        komut.Dispose();

        bağlantı.Close();
        bağlantı.Dispose();

        return sonuc >= 1;
    }
}