﻿using System;
using System.Collections.Generic;
using System.Linq;
using Protocol;
using Protocol.Transport;

namespace ChessConsole.Commands
{
    public class CommandCreateGame : CommandBase
    {
        public override CommandHelpLabel Help { get { return new CommandHelpLabel("cg", "Создать игру"); } }
        public override int ArgsNeed { get { return 0; } }
        public override void DoWork(IEnumerable<string> args)
        {
            if (Utils.CheckArgs(ArgsNeed, args.Count()))
            {
                if (Utils.IsLoggedIn() && Utils.IsNotInGame())
                {
                    var request = new CreateGameRequest {NewPlayer = new User {Name = CurrentUser.Name}};
                    var response = ServerProvider.MakeRequest<CreateGameResponse>(request);
                    if (response.Status == Statuses.Ok)
                    {
                        Console.WriteLine("You create game. ID: " + response.ID);
                        CurrentUser.CurrentGame = response.ID;
                    }
                    else
                    {
                        Console.WriteLine("Bad status");
                    }
                }
            }
        }
    }
}
