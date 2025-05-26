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