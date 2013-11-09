using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace DevExChessConsole
{
    public class EchoProvider
    {
        public void MakeEcho(string echo_str)
        {
            const string echo_url = "http://localhost:2964/Echo?in_str=";
            Console.WriteLine("Santing \"" + echo_str + "\"");
            WebClient client = new WebClient();
            string echo_received = client.DownloadString(echo_url + echo_str);
            Console.Write("Received ");
            if (echo_received==null)
            {
                Console.WriteLine("nothing.");
            }
            else
            {
                Console.WriteLine(echo_received);
            }
        }
    }
}
