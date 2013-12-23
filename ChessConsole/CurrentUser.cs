﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Protocol;
using Protocol.Transport;

namespace ChessConsole
{
    public static class CurrentUser
    {
        public static string Name;
        public static int? CurrentGame;
        public static System.Timers.Timer PulseTimer = new System.Timers.Timer();

        public static void StartPulse()
        {
            PulseTimer.Elapsed += new ElapsedEventHandler(Pulse);
            PulseTimer.Start();
            PulseTimer.Interval = 1000;
        }

        public static void StopPulse()
        {
            PulseTimer.Stop();
        }

        private static void Pulse(object source, ElapsedEventArgs e)
        {
            var command = new PulseRequest();
            command.From = Name;
            var response = ServerProvider.MakeRequest<PulseResponse>(command);
            if (response.Status != Statuses.OK)
            {
                Console.WriteLine("Connection lost!");
                StopPulse();
            }
            if (response.Messages.Count != 0)
            {
                foreach (var element in response.Messages)
                {
                    MessageProcessor.Process(element);
                }
            }
        }
    }
}
