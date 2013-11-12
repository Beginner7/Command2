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
           
            
            const string echo_url = Consts.domain + "/Echo?in_str=";
            string echo_received;
            WebClient client = new WebClient();

            Console.WriteLine("Santing \"" + echo_str + "\" to \"" + Consts.domain + '\"');
            Console.Write("Received ");
            
            try 
            {
                echo_received = client.DownloadString(echo_url + echo_str);
            }
            catch (WebException e)
            {
                echo_received = "error: \"" + e.Message + '\"';
            }
            
            if (echo_received == "")
            {
                echo_received = "nothing.";
            }

            Console.WriteLine(echo_received);
        }
    }
}
