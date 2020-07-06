using Newtonsoft.Json;
using SpotifyNewMusic.Entities.Request;
using SpotifyNewMusic.Entities.Response;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyNewMusic
{
    public class NetworkHelper
    {

        public async static Task<T> SendRequest<T>(BaseRequest requestObj) where T : BaseResponse
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpRequestMessage request = new HttpRequestMessage())
                {
                    request.RequestUri = new Uri(requestObj.url);

                    foreach (KeyValuePair<string, string> keyValuePair in requestObj.headers)
                    {
                        request.Headers.Add(keyValuePair.Key, keyValuePair.Value);
                    }

                    switch (requestObj)
                    {
                        case GetRequest get:
                            request.Method = HttpMethod.Get;
                            break;

                        case PostRequest post:

                            var content = new FormUrlEncodedContent(post.values);
                            content.Headers.Clear();
                            content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                            request.Method = HttpMethod.Post;
                            request.Content = content;

                            break;

                    }

                    using (HttpResponseMessage response = await client.SendAsync(request))
                    {
                        var responseStr = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<T>(responseStr);

                    };
                };
            };

        }

    }
}