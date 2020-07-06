using SpotifyNewMusic.Entities.Spotify;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpotifyNewMusic.Entities.Response
{
    public class GetAlbumsResponse : BaseResponse
    {

        public string href;
        public Album[] items;

    }
}
