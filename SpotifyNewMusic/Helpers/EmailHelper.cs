using SpotifyNewMusic.Entities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace SpotifyNewMusic.Helpers
{
    public class EmailHelper
    {

        public static void EmailNewAlbumInfo(IEnumerable<Program.NewAlbumInfo> newAlbumInfo)
        {
            var smtpClient = new SmtpClient(Config.Current.emailSettings.host)
            {
                Port = Config.Current.emailSettings.port,
                Credentials = new NetworkCredential(Config.Current.emailSettings.emailAddress, "password"),
                EnableSsl = true,
            };

            smtpClient.Send(Config.Current.emailSettings.emailAddress, 
                Config.Current.emailSettings.recipients, 
                "Spotify New Music", 
                "body");
        }

    }
}
