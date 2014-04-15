using System.Text;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System;

namespace Protocol.Transport
{
    public static class ServerProvider
    {
        public static Response MakeRequest(Request r)
        {
            return MakeRequest<Response>(r);
        }
        public static T MakeRequest<T>(Request r)
        {
            try
            {
                const string url = Consts.DOMAIN + "Server.ashx";
                var encoding = Encoding.UTF8;
                string postData = JsonConvert.SerializeObject(r);
                var data = encoding.GetBytes(postData);

                ServicePointManager.Expect100Continue = false;
                var httpWReq = (HttpWebRequest)WebRequest.Create(url);
                httpWReq.Method = "POST";
                httpWReq.ContentType = "text/json";
                httpWReq.ContentLength = data.Length;
                using (Stream stream = httpWReq.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                var response = (HttpWebResponse)httpWReq.GetResponse();

                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                return JsonConvert.DeserializeObject<T>(responseString);
            }
            catch
            {
                Console.WriteLine("Can't connect to server.");
                return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(new Response { RequestCommand = null, Status = Statuses.Unknown })); 
            }
        }
    }
}
