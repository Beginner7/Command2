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
            string command;
            CommandPromt CPromt = new CommandPromt();
            Console.Write("Welcome to Command's 2 Chess Console!\n> ");
            command = Console.ReadLine();
            while (CPromt.CommandProcess(command)) 
            {
                Console.Write("> ");
                command = Console.ReadLine();
            }
        }
    }
}
