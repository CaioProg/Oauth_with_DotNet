using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facebook.Records
{
    public record OauthToken
    {
        public string access_token { get; set; }    
        public string token_type { get; set; }
        public string expires_in { get; set; }
    }
}