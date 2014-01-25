using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol;
using Protocol.GameObjects;

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
                switch (commandWords[0].ToLower())
                {
                    case "":
                        break;

                    case "me":
                        if (Utils.CheckArgs(Commands.CommandMe.ArgsNeed, commandWords.Length - 1))
                        {
                            Commands.CommandMe.Show();
                        }
                        break;

                    case "sb":
                        if (Utils.CheckArgs(Commands.CommandShowBoard.ArgsNeed, commandWords.Length - 1))
                        {
                            Commands.CommandShowBoard.ShowBoard();
                        }
                        break;

                    case "gs":
                        if (Utils.CheckArgs(Commands.CommandGameStats.ArgsNeed, commandWords.Length - 1))
                        {
                            Commands.CommandGameStats.Show();
                        }
                        break;

                    case "echo":
                        if (Utils.CheckArgs(Commands.CommandEcho.ArgsNeed, commandWords.Length - 1))
                        {
                            Commands.CommandEcho.Echo(commandWords.Skip(1).StrJoin(' '));
                        }
                        break;

                    case "say":
                        if (Utils.CheckArgs(Commands.CommandSay.ArgsNeed, commandWords.Length - 1))
                        {
                            Commands.CommandSay.Say(commandWords.Skip(1).StrJoin(' '));
                        }
                        break;

                    case "login":
                        if (Utils.CheckArgs(Commands.CommandLogin.ArgsNeed, commandWords.Length - 1))
                        {
                            Commands.CommandLogin.Login(commandWords[1]);
                        }
                        break;

                    case "logout":
                        if (Utils.CheckArgs(Commands.CommandLogout.ArgsNeed, commandWords.Length - 1))
                        {
                            Commands.CommandLogout.Logout();
                        }
                        break;

                    case "ul":
                        if (Utils.CheckArgs(Commands.CommandUserList.ArgsNeed, commandWords.Length - 1))
                        {
                            Commands.CommandUserList.Show();
                        }
                        break;

                    case "ml":
                        if (Utils.CheckArgs(Commands.CommandMoveList.ArgsNeed, commandWords.Length - 1))
                        {
                            Commands.CommandMoveList.ShowList();
                        }
                        break;

                    case "cg":
                        if (Utils.CheckArgs(Commands.CommandCreateGame.ArgsNeed, commandWords.Length - 1))
                        {
                            Commands.CommandCreateGame.Create();
                        }
                        break;

                    case "disconnect":
                        if (Utils.CheckArgs(Commands.CommandDisconnect.ArgsNeed, commandWords.Length - 1))
                        {
                            Commands.CommandDisconnect.Disconnect();
                        }
                        break;

                    case "gl":
                        if (Utils.CheckArgs(Commands.CommandGameList.ArgsNeed, commandWords.Length - 1))
                        {
                            Commands.CommandGameList.Show();
                        }
                        break;

                    case "jg":
                        if (Utils.CheckArgs(Commands.CommandJoinGame.ArgsNeed, commandWords.Length - 1))
                        {
                            Commands.CommandJoinGame.Join(commandWords[1]);
                        }
                        break;

                    case "move":
                        if (Utils.CheckArgs(Commands.CommandMove.ArgsNeed, commandWords.Length - 1))
                        {
                            Commands.CommandMove.Move(commandWords[1], commandWords[2]);
                        }
                        break;

                    case "help":
                        if (Utils.CheckArgs(Commands.CommandHelp.ArgsNeed, commandWords.Length - 1))
                        {
                            Commands.CommandHelp.ShowHelp();
                        }
                        break;

                    case "exit":
                        if (Utils.CheckArgs(Commands.CommandExit.ArgsNeed, commandWords.Length - 1))
                        {
                            closing = Commands.CommandExit.Exit();
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
