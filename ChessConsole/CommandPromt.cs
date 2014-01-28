using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol;
using Protocol.GameObjects;
using ChessConsole.Commands;

namespace ChessConsole
{
    public static class CommandPromt
    {
        private static bool closing = false;

        public static void CommandProcess()
        {
            while (!closing)
            {
                Console.Write('>');
                string commandInput = Console.ReadLine();
                var commandWords = commandInput.Split(' ');
                /*foreach (var element in CommandFactory.Instance.AllCommands)
                {
                    if (!String.IsNullOrWhiteSpace(commandWords[0]))
                    {
                        bool IsStuffDone = false;
                        if (commandWords[0].ToLower() == element.Help.Name)
                        {
                            IsStuffDone = true;
                        }
                        if (!IsStuffDone)
                        {
                            Console.WriteLine("Unknown command: '" + commandWords[0] + '\'');
                        }
                    }
                }*/
                switch (commandWords[0].ToLower())
                {
                    case "":
                        break;

                    case "me":
                        var commandMe = new CommandMe();
                        if (Utils.CheckArgs(commandMe.ArgsNeed, commandWords.Length - 1))
                        {
                            commandMe.Show();
                        }
                        break;

                    case "sb":
                        var commandShowBoard = new CommandShowBoard();
                        if (Utils.CheckArgs(commandShowBoard.ArgsNeed, commandWords.Length - 1))
                        {
                            commandShowBoard.ShowBoard();
                        }
                        break;

                    case "gs":
                        var commandGameStats = new CommandGameStats();
                        if (Utils.CheckArgs(commandGameStats.ArgsNeed, commandWords.Length - 1))
                        {
                            commandGameStats.Show();
                        }
                        break;

                    case "echo":
                        var commandEcho = new CommandEcho();
                        if (Utils.CheckArgs(commandEcho.ArgsNeed, commandWords.Length - 1))
                        {
                           commandEcho.Echo(commandWords.Skip(1).StrJoin(' '));
                        }
                        break;

                    case "say":
                        var commandSay = new CommandSay();
                        if (Utils.CheckArgs(commandSay.ArgsNeed, commandWords.Length - 1))
                        {
                            commandSay.Say(commandWords.Skip(1).StrJoin(' '));
                        }
                        break;

                    case "login":
                        var commandLogin = new CommandLogin();
                        if (Utils.CheckArgs(commandLogin.ArgsNeed, commandWords.Length - 1))
                        {
                            commandLogin.Login(commandWords[1]);
                        }
                        break;

                    case "logout":
                        var commandLogout = new CommandLogout();
                        if (Utils.CheckArgs(commandLogout.ArgsNeed, commandWords.Length - 1))
                        {
                            commandLogout.Logout();
                        }
                        break;

                    case "ul":
                        var commandUserList = new CommandUserList();
                        if (Utils.CheckArgs(commandUserList.ArgsNeed, commandWords.Length - 1))
                        {
                            commandUserList.Show();
                        }
                        break;

                    case "ml":
                        var commandMoveList = new CommandMoveList();
                        if (Utils.CheckArgs(commandMoveList.ArgsNeed, commandWords.Length - 1))
                        {
                            commandMoveList.ShowList();
                        }
                        break;

                    case "cg":
                        var commandCreateGame = new CommandCreateGame();
                        if (Utils.CheckArgs(commandCreateGame.ArgsNeed, commandWords.Length - 1))
                        {
                            commandCreateGame.Create();
                        }
                        break;

                    case "disconnect":
                        var commandDisconnect = new CommandDisconnect();
                        if (Utils.CheckArgs(commandDisconnect.ArgsNeed, commandWords.Length - 1))
                        {
                            commandDisconnect.Disconnect();
                        }
                        break;

                    case "gl":
                        var commandGameList = new CommandGameList();
                        if (Utils.CheckArgs(commandGameList.ArgsNeed, commandWords.Length - 1))
                        {
                            commandGameList.Show();
                        }
                        break;

                    case "jg":
                        var commandJoinGame = new CommandJoinGame();
                        if (Utils.CheckArgs(commandJoinGame.ArgsNeed, commandWords.Length - 1))
                        {
                            commandJoinGame.Join(commandWords[1]);
                        }
                        break;

                    case "move":
                        var commandMove = new CommandMove();
                        if (Utils.CheckArgs(commandMove.ArgsNeed, commandWords.Length - 1))
                        {
                            commandMove.Move(commandWords[1], commandWords[2]);
                        }
                        break;

                    case "help":
                        var commandHelp = new CommandHelp();
                        if (Utils.CheckArgs(commandHelp.ArgsNeed, commandWords.Length - 1))
                        {
                            commandHelp.ShowHelp();
                        }
                        break;

                    case "exit":
                        var commandExit = new CommandExit();
                        if (Utils.CheckArgs(commandExit.ArgsNeed, commandWords.Length - 1))
                        {
                            closing = commandExit.Exit();
                        }
                        break;

                    default:
                        Console.WriteLine("Unknown command: '" + commandWords[0] + '\'');
                        break;
                }
            }
        }
    }
}
