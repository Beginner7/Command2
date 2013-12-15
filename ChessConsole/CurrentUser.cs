using System;
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
        public static Timer PulseTimer = new Timer();

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
                Console.WriteLine("Connoction lost!");
                StopPulse();
            }
        }
    }
}
