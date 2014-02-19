using System;
using System.Timers;
using Protocol;
using Protocol.Transport;

namespace ChessConsole
{
    public static class CurrentUser
    {
        public static string Name;
        public static int? CurrentGame = null;
        public static Timer PulseTimer = new Timer();
        public static bool NeedPeaseAnswer = false;

        public static void StartPulse()
        {
            PulseTimer.Elapsed += Pulse;
            PulseTimer.Start();
            PulseTimer.Interval = 1000;
        }

        public static void StopPulse()
        {
            PulseTimer.Stop();
        }

        private static void Pulse(object source, ElapsedEventArgs e)
        {
            var command = new PulseRequest {From = Name};
            var response = ServerProvider.MakeRequest<PulseResponse>(command);
            if (response.Status != Statuses.OK)
            {
                Console.WriteLine("Connection lost!");
                if (CurrentGame != null)
                {
                    CurrentGame = null;
                }
                if (Name != null)
                {
                    Name = null;
                }
                StopPulse();
            }

            MessageProcessor.Process(response.Messages);
        }
    }
}
