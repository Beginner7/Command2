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
                AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit);
            AppDomain.CurrentDomain.DomainUnload += new EventHandler(CurrentDomain_ProcessExit);

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

        static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            CommandPromt CPromt = new CommandPromt();
            CPromt.CommandProcess("logout");
        }
    }
}
