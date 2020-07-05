using System;
using System.Collections.Generic;
using System.Text;

namespace SpotifyNewMusic.Entities.Request
{
    public abstract class PostRequest : BaseRequest
    {

        public List<KeyValuePair<string, string>> values = new List<KeyValuePair<string, string>>();

    }
}
