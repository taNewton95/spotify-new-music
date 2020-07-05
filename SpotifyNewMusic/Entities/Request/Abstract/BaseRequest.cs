using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpotifyNewMusic.Entities.Request
{
    public abstract class BaseRequest
    {
        
        public List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>();

        public abstract string url
        {
            get;
        }

    }
}
