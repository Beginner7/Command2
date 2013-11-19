using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace ChessConsole.Transport
{
    public static class ServerProvider
    {
        public static Response MakeRequest(Request r)
        {
            string url = Path.Combine(Consts.domain, "/Server.ashx");
            var encoding = Encoding.UTF8;
            string postData = JsonConvert.SerializeObject(r);
            var data = encoding.GetBytes(postData);

            HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(url);
            httpWReq.Method = "POST";
            httpWReq.ContentType = "text/json";
            httpWReq.ContentLength = data.Length;
            using (Stream stream = httpWReq.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();

            string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            return JsonConvert.DeserializeObject<Response>(responseString);
        }
    }
}
