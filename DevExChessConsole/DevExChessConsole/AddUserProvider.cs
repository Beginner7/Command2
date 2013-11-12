using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DevExChessConsole
{
    public class AddUserProvider
    {
        public void Add(string name)
        {
            string url = Consts.domain + "/User/Login";
            ASCIIEncoding encoding = new ASCIIEncoding();
            string postData = "username=" + name;
            byte[] data = encoding.GetBytes(postData);

            HttpWebRequest httpWReq =(HttpWebRequest)WebRequest.Create(url);
            httpWReq.Method = "POST";
            httpWReq.ContentType = "application/x-www-form-urlencoded";
            httpWReq.ContentLength = data.Length;
            using (Stream stream = httpWReq.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();

            string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
        }

    }
}