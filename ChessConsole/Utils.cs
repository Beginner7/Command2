using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessConsole
{
    public static class Utils
    {
        public static bool IsInGame()
        {
            if (CurrentUser.CurrentGame == null)
            {
                Console.WriteLine("You are not in game.");
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool IsNotInGame()
        {
            if (CurrentUser.CurrentGame != null)
            {
                Console.WriteLine("End current game first!");
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool IsLoggedIn()
        {
            if (CurrentUser.Name == null)
            {
                Console.WriteLine("You are not logged in.");
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool IsNotLoggedIn()
        {
            if (CurrentUser.Name == null)
            {
                return true;
            }
            else
            {
                Console.WriteLine("You are logged in.");
                return false;
            }
        }

        public static bool CheckArgs(int needArgs, int args)
        {
            if (needArgs == -1 && args > 0)
            {
                return true;
            }
            if (needArgs == args)
            {
                return true;
            }
            else
            {
                Console.WriteLine("Wrong arguments count");
                return false;
            }
        }
    }
}
