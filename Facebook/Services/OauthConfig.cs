using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Facebook.Services
{
    public class OauthConfig
    {
        public OauthConfig()
        {
            JToken jAppSetting = JToken.Parse(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "appsettings.json")));
            this.ClientId = jAppSetting["oauth"]["client_id"].ToString();
            this.AppSecret = jAppSetting["oauth"]["app_secret"].ToString();
            this.RedirectUri = jAppSetting["oauth"]["redirect_url"].ToString();
            this.Version = jAppSetting["oauth"]["version"].ToString();
        }

        public object ClientId { get; set; }
        public object AppSecret { get; set; }
        public string RedirectUri { get; set; }
        public string Version { get; set; }
    }
}