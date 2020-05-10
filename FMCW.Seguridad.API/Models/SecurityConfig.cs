using System.Collections.Generic;

namespace FMCW.Seguridad.API.Models
{
    public class SecurityConfig
    { 
        public string Key { get; set; }
        public List<string> Origins { get; set; }
        public string FileSource { get; set; }
    }
}
