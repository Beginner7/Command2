using System;

namespace ChessConsole
{
    public class Program
    {
        static void Main()
        {
            Console.WriteLine("Welcome to Command's 2 Chess Console!");
            Console.WriteLine("Type 'help' to show command list.");
            CommandPromt.CommandProcess();
        }
    }
}
