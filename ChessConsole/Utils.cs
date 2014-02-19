using System;

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
            return true;
        }

        public static bool IsNotInGame()
        {
            if (CurrentUser.CurrentGame != null)
            {
                Console.WriteLine("End current game first!");
                return false;
            }
            return true;
        }

        public static bool IsLoggedIn()
        {
            if (CurrentUser.Name == null)
            {
                Console.WriteLine("You are not logged in.");
                return false;
            }
            return true;
        }

        public static bool IsNotLoggedIn()
        {
            if (CurrentUser.Name == null)
            {
                return true;
            }
            Console.WriteLine("You are logged in.");
            return false;
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
            Console.WriteLine("Wrong arguments count");
            return false;
        }
    }
}
