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
            string command;
            CommandPromt CPromt = new CommandPromt();
            Console.WriteLine("Welcome to Command's 2 Chess Console!");
            command = Console.ReadLine();
            while (CPromt.CommandProcess(command)) 
            {
                command = Console.ReadLine();
            }
        }
    }
}
