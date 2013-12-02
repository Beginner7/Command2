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
                    var echoProvider = new EchoProvider();
                    Console.WriteLine(echoProvider.Echo(command.Skip(1).StrJoin(' ')));
                    break;

                case "adduser":
                    var addUserProvider = new AddUserProvider();
                    Console.WriteLine(addUserProvider.Add(command[1]) ? "success" : "error");
                    break;

                case "deleteuser":
                    var deleteUserProvider = new DeleteUserProvider();
                    Console.WriteLine(deleteUserProvider.Delete(command[1]) ? "success" : "error");
                    break;

                case "userlist":
                    var userListProvider = new UserListProvider();
                    foreach (string element in userListProvider.GetList())
                    {
                        Console.WriteLine(element);
                    }
                    break;

                case "creategame":
                    var createGameProvider = new CreateGameProvider();
                    Console.WriteLine(createGameProvider.Create() ? "success" : "error");
                    break;

                case "gamelist":
                    var gameListProvider = new GameListProvider();
                    foreach (int element in gameListProvider.GetList())
                        Console.WriteLine(element);
                    break;

                case "connecttogame":                  
                    var connectToGameProvider = new ConnectToGameProvider();
                    var gameList = new GameListProvider();
                        Console.WriteLine(connectToGameProvider.Connect(command[1]) ? "success" : "error"); // & gameList.GetList().Contains(command[1])
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
