using System.Collections.Generic;

namespace RoadToCode.Models.Blog
{
    public static class NormalizationProvider
    {
        private static Dictionary<string, string> Map => new Dictionary<string, string>(){
            {" ", "-"},
            {"ą", "a"},
            {"ę", "e"},
            {"ó", "o"},
            {"ś", "s"},
            {"ł", "l"},
            {"ż", "z"},
            {"ź", "z"},
            {"ć", "c"},
            {"ń", "n"},
        };
        
        public static string Normalize(this string s)
        {
            var result = s;
            foreach(var rule in NormalizationProvider.Map)
            {
                result = result.Replace(rule.Key, rule.Value);
            }
            return result;
        }
    }
}