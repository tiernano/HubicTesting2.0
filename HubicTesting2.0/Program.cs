using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HubicTesting2._0
{
    class Program
    {
        static void Main(string[] args)
        {
            using (WebApp.Start<Startup>("http://localhost:12345"))
            {
                Console.ReadLine();
            }
        }
    }

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Run(context =>
            {
                context.Response.ContentType = "text/html";

                switch (context.Request.Path.Value)
                {
                    case "/redirect":
                        var x = context.Request.ReadFormAsync().Result;
                        string clientId = x["clientid"];
                        string redirURL = HttpUtility.UrlEncode(x["redirect"]);
                        string secret = x["secret"];
                        return context.Response.WriteAsync(string.Format("<a href='https://api.hubic.com/oauth/auth/?client_id={0}&redirect_uri={1}&scope=usage.r,account.r,getAllLinks.r,credentials.r,sponsorCode.r,activate.w,sponsored.r,links.drw&response_type=code&state=code'>Click here to authenticate</a>", clientId, redirURL));
                    case "/response/":
                        if(context.Request.Query["error"] != null)
                        {
                            return context.Response.WriteAsync(string.Format("Error: {0} - {1}", context.Request.Query["error"], context.Request.Query["error_description"]));
                        }
                        else
                        {
                            return context.Response.WriteAsync("No errors!");
                        }
                    default:
                        return context.Response.WriteAsync(File.ReadAllText("startpage.html"));

                }                
            });
        }
    }
}
