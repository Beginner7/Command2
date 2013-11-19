using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ChestClient
{
    /// <summary>
    /// Сводное описание для Server
    /// </summary>
    public class Server : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/json";
            var s = new ChessServer.Server();
            var request = new StreamReader(context.Request.InputStream).ReadToEnd();
            context.Response.Write(s.ProcessRequest(request));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}