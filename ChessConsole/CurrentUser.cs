using System;
using System.Timers;
using Protocol;
using Protocol.GameObjects;
using Protocol.Transport;

namespace ChessConsole
{
    public static class CurrentUser
    {
        public static string Name;
        public static int? CurrentGame = null;
        public static Timer PulseTimer = new Timer();
        public static bool NeedPeaseAnswer = false;
        public static bool NeedPawnPromotion = false;
        public static Move LastMove = null;

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
            if (response.Status != Statuses.Ok)
            {
                Console.WriteLine("Connection lost!");
                CurrentGame = null;
                Name = null;
                StopPulse();
            }

            MessageProcessor.Process(response.Messages);
        }
    }
}
