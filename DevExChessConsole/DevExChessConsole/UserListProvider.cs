using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DevExChessConsole
{
    public class UserListProvider
    {
        public IReadOnlyCollection<string> GetList()
        {
            string url = Consts.domain + "User/List";
            string received;

            WebClient client = new WebClient();

            try
            {
                received = client.DownloadString(url);
            }
            catch (WebException e)
            {
                Console.WriteLine("Error:" + e.Message);
                return null;
            }

            List<string> return_list = new List<string>();
            const string TryingFind = "\"UserName\":\"";

            Int32 start_from = received.IndexOf(TryingFind, 0);
            Int32 username_start, username_end = 0, username_length;
            string username;

            while (received[username_end + 2] != ']')
            {
                username_start = start_from + TryingFind.Length;
                username_end = received.IndexOf('\"', username_start);
                username_length = username_end - username_start;
                username = received.Substring(username_start, username_length);
                return_list.Add(username);
                start_from = received.IndexOf(TryingFind, username_end);
            }
            
            return return_list.AsReadOnly();
        }
    }
}