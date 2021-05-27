using System;
using System.Collections.Generic;
using System.Text;

namespace FMCW.Common.Jwt
{
    public class JwtDTO
    {
        public string Jwt { get; set; }
        public DateTime ExpDate { get; set; } 
    }
}
