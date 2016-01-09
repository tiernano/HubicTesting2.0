using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                if (context.Request.Path.Value.ToLower() == "/redirect")
                {
                    var x = context.Request.ReadFormAsync().Result;
                    StringBuilder sb = new StringBuilder();
                    foreach(var y in x)
                    {
                        sb.AppendFormat("{0} - {1}<br>", y.Key, y.Value[0]);
                    }
                    return context.Response.WriteAsync(sb.ToString());
                }
                                
                    return context.Response.WriteAsync(File.ReadAllText("startpage.html"));
                
            });
        }
    }
}
