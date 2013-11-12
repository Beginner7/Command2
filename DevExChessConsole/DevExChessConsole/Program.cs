using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevExChessConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            var addUserProvider = new AddUserProvider();
            addUserProvider.Add("foo");


            EchoProvider echoProvider = new EchoProvider();
            string saysmthng; //Just for test
            Console.WriteLine("What u wanna say?");
            saysmthng=Console.ReadLine();
            echoProvider.MakeEcho(saysmthng);
            Console.ReadKey();
        }
    }
}
