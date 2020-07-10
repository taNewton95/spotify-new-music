using SpotifyNewMusic.Entities;
using SpotifyNewMusic.Entities.Spotify;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace SpotifyNewMusic.Helpers
{
    public class EmailHelper
    {

        private const string _AlbumCard = @"
                    <td>
                        <a href=""@AlbumLink"" class=""Card"">
                            <img alt=""Album Icon"" class=""Album-Icon""
                                src=""@ImageSource"">

                            <label href=""@AlbumLink"" class=""Album-Title"">@AlbumTitle</label>
                            <div class=""Artist-Title"">
                                <label>@ArtistSummary</label>
                            </div>
                        </a>
                    </td>";

        public static void EmailNewAlbumInfo(IEnumerable<Program.NewAlbumInfo> newAlbumInfo)
        {
            var bodyBuilder = new StringBuilder(Properties.Resources.ResourceManager.GetString("EmailTemplate"));

            var cardBuilder = new StringBuilder();

            cardBuilder.Append("<tr>");

            int i = 0;

            foreach (Program.NewAlbumInfo artist in newAlbumInfo.OrderBy(artist => artist.artist, StringComparer.OrdinalIgnoreCase))
            {
                foreach (Album album in artist.albums)
                {
                    if (!string.IsNullOrEmpty(album.album_group) & album.album_group.Equals("appears_on"))
                    {
                        continue;
                    }

                    var _AlbumBuilder = new StringBuilder(_AlbumCard);

                    _AlbumBuilder.Replace("@AlbumLink", album.external_urls.GetValueOrDefault("spotify", ""));
                    _AlbumBuilder.Replace("@ImageSource", album.images[0].url);
                    _AlbumBuilder.Replace("@AlbumTitle", album.name);
                    _AlbumBuilder.Replace("@ArtistSummary", album.album_type.Substring(0,1).ToUpper() + album.album_type.Substring(1) + " by " + artist.artist);

                    cardBuilder.Append(_AlbumBuilder.ToString());

                    i++;

                    // Have a maximum of three columns within the email body. If the row is already filled
                    // move onto the next row in the table.
                    if (i == 3)
                    {
                        i = 0;
                        cardBuilder.Append("</tr>");
                        cardBuilder.Append("<tr>");
                    }

                }
            }

            cardBuilder.Append("</tr>");

            bodyBuilder.Replace("@Body", cardBuilder.ToString());

            var smtpClient = new SmtpClient(Config.Current.emailSettings.host)
            {
                Port = Config.Current.emailSettings.port,
                Credentials = new NetworkCredential(Config.Current.emailSettings.emailAddress, Config.Current.emailSettings.password),
                EnableSsl = true,
            };

            var message = new MailMessage(Config.Current.emailSettings.emailAddress,
                Config.Current.emailSettings.recipients,
                $"Spotify New Music ({DateTime.Now.ToString("ddd dd MMMM yyyy")})",
                bodyBuilder.ToString());

            message.IsBodyHtml = true;

            smtpClient.Send(message);
        }

    }
}
