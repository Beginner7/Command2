using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevExChessConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            EchoProvider echoProvider = new EchoProvider();
            string saysmthng; //Just for test
            Console.WriteLine("What u wanna say?");
            saysmthng=Console.ReadLine();
            echoProvider.MakeEcho(saysmthng);
            Console.ReadKey();
        }
    }
}
