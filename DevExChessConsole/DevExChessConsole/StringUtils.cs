using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessConsole
{
    public static class StringUtils
    {
        public static string StrJoin(this IEnumerable<string> strs)
        {
            var sb = new StringBuilder();
            foreach (var s in strs) {
                sb.Append(s);
            }
            return sb.ToString();
        }
    }
}
