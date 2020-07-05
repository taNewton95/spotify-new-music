using SpotifyNewMusic.Entities;
using SpotifyNewMusic.Entities.Enums;
using SpotifyNewMusic.Entities.Request;
using System;

namespace SpotifyNewMusic
{
    class Program
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

            await APIHelper.GetAlbums(ArtistMap.Aitch);

        }

    }
}
