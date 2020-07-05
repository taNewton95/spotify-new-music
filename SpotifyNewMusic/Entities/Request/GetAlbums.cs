using System;
using System.Collections.Generic;
using System.Text;

namespace SpotifyNewMusic.Entities.Request
{
    public class GetAlbums : GetRequest
    {

        public readonly string artistId;

        public override string url => $"https://api.spotify.com/v1/artists/{artistId}/albums";

        public GetAlbums(string artistId)
        {
            this.artistId = artistId;
        }

    }
}
