using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace FMCW.Common
{
    public static class Helpers
    {
        public static Dictionary<string, string> ReplaceableValues = new Dictionary<string, string>
        {
            { "ß", "ss"},
            { "ä", "a"}, { "á", "a"},
            { "ë", "e"}, { "é", "e"},
            { "ï", "i"}, { "í", "i"},
            { "ö", "o"}, { "ó", "o"},
            { "ü", "u"}, { "ú", "u"}
        };

        public static string GetSearchValue(string str)
        {
            if (string.IsNullOrEmpty(str)) return str;

            str = str.ToLower();
            foreach (var replace in ReplaceableValues)
            {
                str = str.Replace(replace.Key, replace.Value);
            }
            return str;
        }

        public static string GetRandomCode(int n)
        {
            Random generator = new Random();
            return generator.Next(0, (int)Math.Pow(10, n)).ToString("D" + n);
        }

        public static string GetExpresionFecha(this DateTime fecha)
        {
            if (fecha.Date == DateTime.Today) return "hoy";
            else if (fecha.Date == DateTime.Today.AddDays(1)) return "mañana";
            else return "el " + fecha.ToString("dd/MM");
        }

        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
