using System;
using System.Collections.Generic;
using System.Text;

namespace SpotifyNewMusic.Entities.Request
{
    public class Authorize : PostRequest
    {

        public override string url => "https://accounts.spotify.com/api/token";

    }
}
