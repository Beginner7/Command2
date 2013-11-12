using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevExChessConsole
{
    class CommandPromt
    {
        protected string cut_word(string in_string)
        {
            return in_string;
        }

        public void CommandProcess(string in_command)
        {
            string command = cut_word(in_command);
        }
    }
}
