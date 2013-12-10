﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessConsole
{

    class CommandPromt
    {
        Protocol.Board gameboard = new Protocol.Board();
        public CommandPromt()
        {
            gameboard.InitialPosition();
        }
        

        public bool CommandProcess(string in_command)
        {
            bool is_continue = true;
            var command = in_command.Split(' ');
            switch (command[0])
            {
                case "":
                    break;

                case "me":
                    if (CurrentUser.Name != null)
                    {
                        Console.WriteLine("You are logged as: " + CurrentUser.Name);
                    }
                    else
                    {
                        Console.WriteLine("You are not logged in.");
                    }
                    if (CurrentUser.CurrentGame != null)
                    {
                        Console.WriteLine("You are in game. ID: " + CurrentUser.CurrentGame);
                    }
                    else
                    {
                        Console.WriteLine("You are not in game.");
                    }
                    break;

                case "sb":
                    gameboard.ShowBoard();
                    break;

                case "gamestat":
                    if (CurrentUser.CurrentGame == null)
                    {
                        Console.WriteLine("Dude! First connect to game.");
                        break;
                    }
                    var gameStatProvider = new GameStatProvider(CurrentUser.CurrentGame.Value);
                    break;

                case "echo":
                    var echoProvider = new EchoProvider();
                    Console.WriteLine(echoProvider.Echo(command.Skip(1).StrJoin(' ')));
                    break;

                case "login":
                    var addUserProvider = new AddUserProvider();

                    if (CurrentUser.Name != null)
                    {
                        Console.WriteLine("logout first.");
                        break;
                    }

                    if (command.Length < 2 || string.IsNullOrWhiteSpace(command[1]))
                    {
                        Console.WriteLine("Empty user name.");
                        break;
                    }
                    if (addUserProvider.Add(command[1]))
                    {
                        CurrentUser.Name = command[1];
                        Console.WriteLine("Hello, " + CurrentUser.Name);
                    }
                    else
                    {
                        Console.WriteLine("User " + command[1] + " already logged in.");
                    }
                    break;

                case "logout":
                    var deleteUserProvider = new DeleteUserProvider();
                    if (CurrentUser.Name != null)
                    {
                        if (deleteUserProvider.Delete(CurrentUser.Name))
                        {
                            Console.WriteLine("You succesfully logged out.");
                            CurrentUser.Name = null;
                        }
                        else
                        {
                            Console.WriteLine("Unknown error.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("You are not logged in.");
                    }
                    break;

                case "userlist":
                    var userListProvider = new UserListProvider();
                    Console.WriteLine("Users online:");
                    foreach (string element in userListProvider.GetList())
                    {
                        Console.WriteLine(element);
                    }
                    break;

                case "creategame":
                    var createGameProvider = new CreateGameProvider();
                    if (CurrentUser.Name == null)
                    {
                        Console.WriteLine("You are not logged in.");
                        break;
                    }
                    if (CurrentUser.CurrentGame != null)
                    {
                        Console.WriteLine("You are in the game already.");
                        break;
                    }
                    var gameID = createGameProvider.Create(CurrentUser.Name);
                    if (!gameID.HasValue)
                    {
                        Console.WriteLine("Can't create game");
                    }
                    else
                    {
                        Console.WriteLine("Successfully created game with id " + gameID + '.');
                        CurrentUser.CurrentGame = gameID;
                    }
                    break;

                case "gamelist":
                    var gameListProvider = new GameListProvider();
                    Console.WriteLine("Active games:");
                    foreach (int element in gameListProvider.GetList())
                    {
                        Console.WriteLine(element);
                    }
                    break;

                case "connecttogame":                  
                    var connectToGameProvider = new ConnectToGameProvider();
                    if (CurrentUser.Name == null)
                    {
                        Console.WriteLine("You are not logged in.");
                        break;
                    }
                    if (command.Length < 2 || string.IsNullOrWhiteSpace(command[1]))
                    {
                        Console.WriteLine("Empty game id.");
                        break;
                    }
                    if (CurrentUser.CurrentGame != null)
                    {
                        Console.WriteLine("You are in the game already.");
                        break;
                    }
                    int gameid;
                    try
                    {
                        gameid = Convert.ToInt32(command[1]);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Game id should be integer number.");
                        break;
                    }
                    if (connectToGameProvider.Connect(gameid, CurrentUser.Name)) {
                        CurrentUser.CurrentGame = gameid;
                        Console.WriteLine("You joined game " + gameid);
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
                    moveProvider.Move(command[1], command[2], CurrentUser.Name, CurrentUser.CurrentGame.Value);
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
