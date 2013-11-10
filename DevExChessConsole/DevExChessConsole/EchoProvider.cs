﻿#define DEBUG

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
            #if (DEBUG)
                const string domain = "http://localhost:2964/";
            #else
                const string domain = "http://command2.apphb.com/";
            #endif
            
            const string echo_url = domain + "/Echo?in_str=";
            string echo_received;
            WebClient client = new WebClient();

            Console.WriteLine("Santing \"" + echo_str + "\" to \"" + domain + '\"');
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
