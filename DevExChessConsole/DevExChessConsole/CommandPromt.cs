using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessConsole
{

    class CommandPromt
    {
        public bool CommandProcess(string in_command)
        {
            bool is_continue = true;
            var command = in_command.Split(' ');
            switch (command[0])
            {
                case "":
                    break;

                case "echo":
                    EchoProvider echoProvider = new EchoProvider();
                    echoProvider.MakeEcho(command.Skip(1).StrJoin());
                    break;

                case "adduser":
                    AddUserProvider addUserProvider = new AddUserProvider();
                    Console.WriteLine(addUserProvider.Add(command[1]) ? "success" : "error");
                    break;

                case "userlist":
                    UserListProvider userListProvider = new UserListProvider();
                    foreach (string element in userListProvider.GetList())
                    {
                        Console.WriteLine(element);
                    }
                    break;

                case "exit":
                    is_continue = false;
                    break;

                default:
                    Console.WriteLine("Unknown command: \"" + command[0] + '\"');
                    break;
            }

            return is_continue;
        }
    }
}
