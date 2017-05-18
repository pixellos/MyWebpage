using System.Collections.Generic;
using System.Linq;
using System;

namespace RoadToCode.Services.Blog
{
    public class UnderscoredLinkifier
    {
        private Dictionary<char, char> Replacements = new Dictionary<char, char>()
        {
            {' ', '_'}
        };

        public string Linkify(string fromString)
        {
            if (fromString.Any(c => !char.IsLetterOrDigit(c) || !char.IsWhiteSpace(c) || c != '-'))
            {
                throw new ArgumentException(nameof(fromString));
            }
            var result = fromString;
            foreach (var replacement in this.Replacements)
            {
                result = result.Replace(replacement.Key, replacement.Value);
            }
            return result;
        }

        public string UnLinkify(string fromString)
        {
            var result = fromString;
            foreach (var replacement in this.Replacements)
            {
                result = result.Replace(replacement.Value, replacement.Key);
            }
            return result;
        }
    }
}