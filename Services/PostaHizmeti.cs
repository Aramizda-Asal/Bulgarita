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
using System.Text;
using bulgarita.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;
using MailKit.Net.Smtp;
using MailKit.Net.Imap;
using MailKit;
using MimeKit;
using MailKit.Security;

namespace bulgarita.Services;

public static class PostaFonksiyonları
{
    public static bool EPostaGönder(
        string başlık,
        string içerik,
        List<Kullanıcı> alıcılar
    )
    {
        if (alıcılar.Count == 0)
            return false;

        JObject gönderen;
        JToken smtp, imap;
        SmtpClient SMTP_istemci;
        ImapClient IMAP_istemci;

        try
        {
            string posta_json = System.IO.File.ReadAllText("Ayarlar/posta.json");
            gönderen = JObject.Parse(posta_json);
            smtp = gönderen["smtp"];
            imap = gönderen["imap"];

            SMTP_istemci = new SmtpClient();
            IMAP_istemci = new ImapClient();

            SMTP_istemci.Connect(
                smtp["host"].Value<String>(),
                smtp["port"].Value<Int32>(),
                MailKit.Security.SecureSocketOptions.Auto
            );
            IMAP_istemci.Connect(
                imap["host"].Value<String>(),
                imap["port"].Value<Int32>(),
                MailKit.Security.SecureSocketOptions.Auto
            );

            JToken auth = gönderen["auth"];
            string auth_type = auth["type"].Value<String>();

            if (auth_type == "oauth2")
            {
                var oauth2 = new SaslMechanismOAuth2(
                    auth["email"].Value<String>(),
                    auth["access_token"].Value<String>()
                );

                SMTP_istemci.Authenticate(oauth2);
                IMAP_istemci.Authenticate(oauth2);
            }
            else if (auth_type == "password")
            {
                SMTP_istemci.Authenticate(
                    auth["username"].Value<String>(),
                    auth["password"].Value<String>()
                );

                IMAP_istemci.Authenticate(
                    auth["username"].Value<String>(),
                    auth["password"].Value<String>()
                );
            }
            else
            {
                Console.WriteLine(
                    "Bu kimlik doğrulama yöntemi desteklenmiyor: {0}",
                    auth_type
                );
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }

        int hata_niceliği = 0;
        foreach (Kullanıcı k in alıcılar)
        {
            try
            {
                var ileti = new MimeMessage();
                ileti.From.Add(
                    new MailboxAddress("Bulgaristan Yer Adları", smtp["address"].Value<String>())
                );
                ileti.To.Add(new MailboxAddress(k.Adı, k.E_posta));

                ileti.Subject = başlık;
                ileti.Body = new TextPart("plain")
                {
                    Text = içerik
                };

                SMTP_istemci.Send(ileti);


                IMailFolder gönderilen;

                if (IMAP_istemci.Capabilities.HasFlag(ImapCapabilities.SpecialUse))
                {
                    gönderilen = IMAP_istemci.GetFolder(SpecialFolder.Sent);
                }
                else
                {
                    var kişisel = IMAP_istemci.GetFolder(IMAP_istemci.PersonalNamespaces[0]);
                    gönderilen = kişisel.GetSubfolder("Sent");
                }

                gönderilen.Append(ileti, MessageFlags.Seen);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                hata_niceliği++;
                continue;
            }
        }

        SMTP_istemci.Disconnect(true);
        SMTP_istemci.Dispose();
        IMAP_istemci.Disconnect(true);
        IMAP_istemci.Dispose();

        if (hata_niceliği < alıcılar.Count)
            return true;
        return false;
    }
}