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

                case "login":
                    var addUserProvider = new AddUserProvider();

                    if (CurrentUser.Name != null)
                    {
                        Console.WriteLine("logout first!");
                        break;
                    }

                    if (command.Length < 2 || string.IsNullOrWhiteSpace(command[1]))
                    {
                        Console.WriteLine("Empty user name");
                        break;
                    }
                    if (addUserProvider.Add(command[1]))
                    {
                        CurrentUser.Name = command[1];
                        Console.WriteLine("Hello, " + CurrentUser.Name);
                    }
                    else
                    {
                        Console.WriteLine("User " + CurrentUser.Name + " already logged in");
                    }
                    break;

                case "logout":
                    var deleteUserProvider = new DeleteUserProvider();
                    if (CurrentUser.Name != null)
                    {
                        Console.WriteLine(deleteUserProvider.Delete(CurrentUser.Name) ? "success" : "error");
                    }
                    else
                    {
                        Console.WriteLine("You are not logged in");
                    }
                    CurrentUser.Name = null;
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
                    if (CurrentUser.Name == null)
                    {
                        Console.WriteLine("You are not logged in");
                        break;
                    }
                    if (CurrentUser.CurrentGame != null)
                    {
                        Console.WriteLine("You are in the game already");
                        break;
                    }
                    var gameID = createGameProvider.Create(CurrentUser.Name);
                    if (!gameID.HasValue)
                    {
                        Console.WriteLine("Can't create game");
                    }
                    else
                    {
                        Console.WriteLine("Successfully created game with id " + gameID);
                        CurrentUser.CurrentGame = gameID;
                    }
                    break;

                case "gamelist":
                    var gameListProvider = new GameListProvider();
                    foreach (int element in gameListProvider.GetList())
                        Console.WriteLine(element);
                    break;

                case "connecttogame":                  
                    var connectToGameProvider = new ConnectToGameProvider();
                    if (CurrentUser.Name == null)
                    {
                        Console.WriteLine("You are not logged in");
                        break;
                    }
                    if (command.Length < 2 || string.IsNullOrWhiteSpace(command[1]))
                    {
                        Console.WriteLine("Empty game id");
                        break;
                    }
                    if (CurrentUser.CurrentGame != null)
                    {
                        Console.WriteLine("You are in the game already");
                        break;
                    }
                    string message;
                    if (connectToGameProvider.Connect(command[1], CurrentUser.Name, out message)) {
                        CurrentUser.CurrentGame = int.Parse(command[1]);
                        Console.WriteLine("success");
                    }
                    else {
                        Console.WriteLine(message);
                    }
                    break;
                    
                case "move":
                    var moveProvider = new MoveProvider();
                    if (CurrentUser.Name == null)
                    {
                        Console.WriteLine("You are not logged in");
                        break;
                    }
                    if (CurrentUser.CurrentGame == null)
                    {
                        Console.WriteLine("Dude! First connect to game.");
                        break;
                    }
                    if (command.Length < 3 || string.IsNullOrWhiteSpace(command[1]) || string.IsNullOrWhiteSpace(command[2]))
                    {
                        Console.WriteLine("Incorrect syntax. Example(move e2 e4)");
                        break;
                    }
                    if (moveProvider.Move(command[1], command[2], CurrentUser.Name, CurrentUser.CurrentGame.Value))
                    {
                        Console.WriteLine("success");
                    }
                    else Console.WriteLine("Error");
                    
                    break;
                case "help":
                    Console.WriteLine("echo <echo_string> - Эхо запрос на сервер");
                    Console.WriteLine("login <user_name>  - Вход на сервер под ником user_name");
                    Console.WriteLine("logout             - Выход из аккаунта");
                    Console.WriteLine("userlist           - Список вошедших пользователей");
                    Console.WriteLine("creategame         - Добавьте описание!");
                    Console.WriteLine("gamelist           - Добавьте описание!");
                    Console.WriteLine("connecttogame      - Добавьте описание!");
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
