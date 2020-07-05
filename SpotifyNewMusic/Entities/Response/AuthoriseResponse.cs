using System;
using System.Collections.Generic;
using System.Text;

namespace SpotifyNewMusic.Entities.Response
{
    public class AuthoriseResponse : BaseResponse
    {

        public string access_token, token_type;
        public int expires_in;

    }
}
