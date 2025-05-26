/*
 * Copyright (C) 2025 Güneş Balcı, Habil Tataroğulları, Yusuf Kozan
 *
 * This file is part of "Bulgaristan’da Harita Bazlı Yer İsimleri Uygulaması".
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Affero General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Affero General Public License for more details.
 * 
 * You should have received a copy of the GNU Affero General Public License
 * along with this program.  If not, see <https://www.gnu.org/licenses/>.
 * 
 */

using System;
using System.IO;
using MySql.Data.MySqlClient;

namespace bulgarita.Services;

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
    internal static string Roller_Tablosu = tablolar[5];
}
 