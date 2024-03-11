using System;
using System.IO;
using MySql.Data.MySqlClient;

namespace bulgarita;

internal class Bağlantı
{
    internal static string[] MySQL = File.ReadAllLines("Ayarlar/VT1.txt");
    internal static string bağlantı_dizisi = $"server={MySQL[0]};port={MySQL[1]};userid={MySQL[2]};password={MySQL[3]};database={MySQL[4]}";     
}
