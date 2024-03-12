using System;
using System.IO;
using MySql.Data.MySqlClient;

namespace bulgarita;

internal class Bağlantı
{
    internal static string[] MySQL = File.ReadAllLines("Ayarlar/VT1.txt");
    internal static string bağlantı_dizisi = $"server={MySQL[0]};port={MySQL[1]};userid={MySQL[2]};password={MySQL[3]};database={MySQL[4]}";     

    internal static string[] tablolar = File.ReadAllLines("Ayarlar/VT2.txt");

    internal static string Kullanıcı_Tablosu = tablolar[0];
    internal static string Oturum_Tablosu = tablolar[1];
    internal static string Harita_Tablosu = tablolar[2];
    internal static string Favoriler_Tablosu = tablolar[3];
    internal static string Bilgi_Doğrulama_Tablosu = tablolar[4];
}
 