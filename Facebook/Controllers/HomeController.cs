using System.Net;
using Facebook.Records;
using Facebook.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Facebook.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [Route("/")]
    public ActionResult Index()
    {
        return Redirect("/swagger");
    }

    [HttpGet]
    [Route("/facebook-oauth")]
    public ActionResult FacebookOauth(string code = "")
    {
        var oauthConfig = new OauthConfig();
        if(string.IsNullOrEmpty(code))
        {
            return Redirect($"https://graph.facebook.com/oauth/access_token?client_id={oauthConfig.ClientId}&redirect_uri={oauthConfig.RedirectUri}");
        }

        return Redirect($"https://graph.facebook.com/{oauthConfig.Version}/oauth/access_token?client_id={oauthConfig.ClientId}&client_secret={oauthConfig.ClientId}&redirect_uri={oauthConfig.RedirectUri}&client_secret={oauthConfig.AppSecret}&code={code}");

        string json = string.Empty;
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://graph.facebook.com/{oauthConfig.Version}/oauth/access_token?client_id={oauthConfig.ClientId}&client_secret={oauthConfig.ClientId}&redirect_uri={oauthConfig.RedirectUri}&client_secret={oauthConfig.AppSecret}&code={code}");
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        if(response.StatusCode == HttpStatusCode.OK)
        {
            Stream receiveStream = response.GetResponseStream();
            StreamReader readStream = new StreamReader(receiveStream);
            json = readStream.ReadToEnd();
            response.Close();
            readStream.Close();
        }

        var oauthToken = JsonConvert.DeserializeObject<OauthToken>(json);

        return Redirect($"https://graph.facebook.com/me?fields=name,gender,email,birthday&access_token={oauthToken.access_token}");
    }

}
