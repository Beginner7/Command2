﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Protocol;
using Protocol.Transport;

namespace ChessConsole
{
    public class DeleteUserProvider
    {
        public bool Delete(string name)
        {
            var command = new DeleteUserRequest();
            command.UserName = name;
            var response = ServerProvider.MakeRequest(command);
            return response.Status == Statuses.OK;
        }
    }
}