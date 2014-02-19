using System.Collections.Generic;
using System.Text;

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
