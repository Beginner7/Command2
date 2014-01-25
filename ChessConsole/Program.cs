using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Command's 2 Chess Console!");
            Console.WriteLine("Type 'help' to show command list.");
            CommandPromt.CommandProcess();
        }
    }
}
