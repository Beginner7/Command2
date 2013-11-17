using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevExChessConsole
{
    class CommandPromt
    {
        protected string get_word(string in_str)
        {
            in_str = in_str.Trim(' ');

            if (in_str.IndexOf(' ') < 0)
            {
                return in_str;
            }

            in_str = in_str.Substring(0, in_str.IndexOf(' '));

            return in_str.Trim();
        }

        protected string delete_word(string in_str)
        {
            in_str = in_str.Trim(' ');

            if (in_str.IndexOf(' ') < 0)
            {
                return null;
            }

            in_str = in_str.Substring(in_str.IndexOf(' '));

            return in_str.Trim();
        }

        public bool CommandProcess(string in_command)
        {
            bool is_continue = true;
            switch (get_word(in_command))
            {
                case "":
                    break;

                case "echo":
                    EchoProvider echoProvider = new EchoProvider();
                    echoProvider.MakeEcho(delete_word(in_command));
                    break;

                case "adduser":
                    AddUserProvider addUserProvider = new AddUserProvider();
                    addUserProvider.Add(get_word(delete_word(in_command)));
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
                    Console.WriteLine("Unknown command: \"" + get_word(in_command) + '\"');
                    break;
            }

            return is_continue;
        }
    }
}
