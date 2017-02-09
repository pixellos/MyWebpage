using System;
using System.Linq;

namespace RoadToCode.Module.Blog
{
    public static class HumanReadableUrl
    {
        public static string GetLinkString(this string fromString)
        {
            return String.Concat(
                fromString.Where(
                    c => (char.IsLetterOrDigit(c) ||
                          char.IsWhiteSpace(c) ||
                          c == '-'))).Replace(' ', '-');
        }

        public static string UnLinkString(this string fromString)
        {
            return fromString.Replace('-', ' ');
        }

      
    }
}