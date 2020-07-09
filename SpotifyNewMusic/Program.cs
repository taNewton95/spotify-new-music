using SpotifyNewMusic.Entities;
using SpotifyNewMusic.Entities.Request;
using SpotifyNewMusic.Entities.Response;
using SpotifyNewMusic.Entities.Spotify;
using SpotifyNewMusic.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SpotifyNewMusic
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (Config.Current == null || string.IsNullOrEmpty(Config.Current.client_id))
            {
                Console.WriteLine($"Invalid config, modify {Config._ConfigDir + "\\" + Config._FileName}");
                Console.WriteLine();
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
                return;
            }

            run();
            Console.Read();
        }

        private static async void run()
        {
            await APIHelper.Authorise();

            var allNewAlbums = new List<NewAlbumInfo>();

            foreach (KeyValuePair<string, string> artist in Config.Current.artists)
            {
                GetAlbumsResponse response = await APIHelper.GetAlbums(artist.Value);

                var newAlbums = response.items.Where(album => album.release_date.CompareTo(DateTime.Now.Date.AddDays(-1)) > 0);

                if (newAlbums.Count() == 0)
                {
                    continue;
                }

                NewAlbumInfo newAlbumInfo = new NewAlbumInfo();
                newAlbumInfo.artist = artist.Key;
                newAlbumInfo.albums = newAlbums;
                allNewAlbums.Add(newAlbumInfo);

            }

            EmailHelper.EmailNewAlbumInfo(allNewAlbums);

            Environment.Exit(0);

        }

        public class NewAlbumInfo
        {
            public string artist;
            public IEnumerable<Album> albums;

        }

    }
}
