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

using Newtonsoft.Json;

namespace bulgarita.Models;

public class Kullanıcı
{
    public string Kimlik;
    public string Adı;
    [JsonIgnore]
    public string Şifre;
    public string E_posta;
    internal Kullanıcı() {}
    internal Kullanıcı(string Adı, string E_posta, string Şifre, string Kimlik)
    {
        this.Adı = Adı;
        this.E_posta = E_posta;
        this.Şifre = Şifre;
        this.Kimlik = Kimlik;
    }
}