using System;
using System.Security.Cryptography;
using Konscious.Security.Cryptography;
using System.Text;
using System.ComponentModel;

namespace bulgarita.Services;

public static class Parolalar
{
    private static string ayraç = "üğ₺";
    
    public static byte[] Tuz()
    {
        RandomNumberGenerator üreteç = RandomNumberGenerator.Create();
        byte[] tuz = new byte[32];
        üreteç.GetBytes(tuz);
        return tuz;
    }

    public static string KarılmışParola(string girilen, byte[] tuz)
    {
        byte[] girilenB = Encoding.Unicode.GetBytes(girilen);
        Argon2id karıcı = new Argon2id(girilenB);
        karıcı.Salt = tuz;
        karıcı.DegreeOfParallelism = 2;
        karıcı.Iterations = 8;
        karıcı.MemorySize = 32*1024;
        byte[] karma = karıcı.GetBytes(30);

        StringBuilder sonuç = new StringBuilder();
        sonuç.Append(Convert.ToBase64String(karma));
        sonuç.Append(ayraç);
        sonuç.Append(Convert.ToBase64String(tuz));
        sonuç.Append(ayraç);
        sonuç.Append(karıcı.DegreeOfParallelism);
        sonuç.Append(',');
        sonuç.Append(karıcı.Iterations);
        sonuç.Append(',');
        sonuç.Append(karıcı.MemorySize);
        sonuç.Append(',');
        sonuç.Append(karma.Length);
        return Temizlik.YolaUydur(sonuç.ToString());
    }

    public static bool ParolaDoğru(string girilen, string bilinen)
    {
        string[] ParolaBilgiler = bilinen.Split(ayraç);
        string[] ParolaAyarlar = ParolaBilgiler[2].Split(',');
        int[] ayarlar = new int[ParolaAyarlar.Length];

        for(int i = 0; i < ParolaAyarlar.Length; i++)
        {
            if (!int.TryParse(ParolaAyarlar[i], out ayarlar[i]))
            {
                return false;
            }
        }

        byte[] girilenB = Encoding.Unicode.GetBytes(girilen);
        Argon2id karıcı = new Argon2id(girilenB);
        karıcı.Salt = Convert.FromBase64String(Temizlik.YolaUygundanBase64(ParolaBilgiler[1]));
        karıcı.DegreeOfParallelism = ayarlar[0];
        karıcı.Iterations = ayarlar[1];
        karıcı.MemorySize = ayarlar[2];
        string girilenin_karması = Convert.ToBase64String(karıcı.GetBytes(ayarlar[3]));
        girilenin_karması = Temizlik.YolaUydur(girilenin_karması);

        return String.Equals(girilenin_karması, ParolaBilgiler[0]);
    }
}