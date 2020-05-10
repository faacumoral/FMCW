using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FMCW.Common
{
    public static class Helpers
    {
        public static T ReadJson<T>(string path)
        {
            if (!File.Exists(path))
            {
                path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
            }
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(path)); ;
        }
    }
}
