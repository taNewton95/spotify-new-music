using SpotifyNewMusic.Entities;
using SpotifyNewMusic.Entities.Request;
using SpotifyNewMusic.Entities.Response;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace SpotifyNewMusic
{
    public class APIHelper
    {

        private static string _AccessToken;

        public async static Task<bool> Authorise()
        {
            Authorize authorize = new Authorize();

            byte[] utf8Bytes = Encoding.UTF8.GetBytes($"{Config.Current.client_id}:{Config.Current.client_secret}");

            authorize.values.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
            authorize.headers.Add(new KeyValuePair<string, string>("Authorization", $"Basic {Convert.ToBase64String(utf8Bytes)}"));

            AuthoriseResponse response = await NetworkHelper.SendRequest<AuthoriseResponse>(authorize);

            _AccessToken = response.access_token;

            return true;
        }

        public async static Task<GetAlbumsResponse> GetAlbums(string artistId)
        {
            GetAlbums getAlbums = new GetAlbums(artistId);

            getAlbums.headers.Add(new KeyValuePair<string, string>("Authorization", $"Bearer {_AccessToken}"));

            return await NetworkHelper.SendRequest<GetAlbumsResponse>(getAlbums);

        }

    }
}
