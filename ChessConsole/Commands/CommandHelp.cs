using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessConsole.Commands
{
    public static class CommandHelp
    {
        public static int ArgsNeed = 0;

        private static List<string> _HelpStrings = new List<string>();

        static CommandHelp()
        {
            _HelpStrings.Add("help                            - Помощь по коммандам (Эта самая)");
            _HelpStrings.Add("login <user name>               - Вход а аккаунт");
            _HelpStrings.Add("logout                          - Выход из аккаунта");
            _HelpStrings.Add("me                              - Состояние аккаунта");
            _HelpStrings.Add("gl                              - Список игр");
            _HelpStrings.Add("cg                              - Создать игру");
            _HelpStrings.Add("jg <game id>                    - Присоединиться к игре по номеру");
            _HelpStrings.Add("disconnect                      - Покинуть игру");
            _HelpStrings.Add("move <start cell> <target cell> - Сделать ход");
            _HelpStrings.Add("say <message>                   - Отправить сообщение оппоненту");
            _HelpStrings.Add("ml                              - Показать лог игры");
            _HelpStrings.Add("sb                              - Отобразить доску");
            _HelpStrings.Add("gs                              - Отобразить состояния игры");
            _HelpStrings.Add("ul                              - Список вошедших пользователей");
            _HelpStrings.Add("echo <echo string>              - Эхо запрос на сервер");
            _HelpStrings.Add("exit                            - Выйти");  
        }

        public static void ShowHelp()
        {
            foreach (var element in _HelpStrings)
            {
                Console.WriteLine(element);
            }
        }
    }
}
