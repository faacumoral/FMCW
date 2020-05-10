using System;
using System.Collections.Generic;
using System.Text;

namespace FMCW.DTO.Seguridad
{
    public class GoogleSecretDTO : BaseDTO
    {
        public string ClientId { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;
    }
}
