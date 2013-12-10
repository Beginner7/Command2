using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessConsole
{
    public static class StringUtils
    {
        public static string StrJoin(this IEnumerable<string> strs, char separator)
        {
            var sb = new StringBuilder();
            foreach (var s in strs) {
                sb.Append(s);
                sb.Append(separator);
            }
            return sb.ToString().Substring(0, sb.Length - 1);
        }
    }
}
