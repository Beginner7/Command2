﻿using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol;
using Newtonsoft.Json;

namespace ChessServer.Commands
{
    public class CommandGameStat : CommandBase
    {
        public override string Name { get { return "gamestat"; } }
        public override Response DoWork(string request, ref ConcurrentDictionary<string, User> users, ref ConcurrentDictionary<int, Game> games)
        {
            var workRequest = JsonConvert.DeserializeObject<GameStatRequest>(request);
            var workResponse = new GameStatResponse();
            workResponse.ID = workRequest.gameID;
            workResponse.Act = games[workRequest.gameID].act;
            if (games[workRequest.gameID].PlayerBlack != null)
            {
                workResponse.PlayerBlack = games[workRequest.gameID].PlayerBlack.Name;
            }
            if (games[workRequest.gameID].PlayerWhite != null)
            {
                workResponse.PlayerWhite = games[workRequest.gameID].PlayerWhite.Name;
            }
            workResponse.Turn = games[workRequest.gameID].Turn;
            workResponse.Status = Statuses.OK;
            return workResponse;
        }
    }
}