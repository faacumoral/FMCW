using System.Collections.Generic;
using System.Linq;
using System.Net;
using FMCW.Common;
using FMCW.Common.Results;
using FMCW.DTO.Seguridad;
using FMCW.Seguridad.API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Files = System.IO.File;

namespace FMCW.Seguridad.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoogleController : ControllerBase
    {
        public ActionResult Get(string key)
        {
            return Ok(new { Host = HttpContext.Connection.LocalIpAddress.ToString()});
            const string PATH_CONFIG = "config/key";
            var password = Files.ReadAllText(PATH_CONFIG);

            var configs = Helpers.ReadJson<List<SecurityConfig>>("Config/config.json");
            var config = configs.First(c => c.Key == key);

            var jsonEncriptado = Files.ReadAllText(config.FileSource);
            var result = JsonConvert.DeserializeObject<GoogleSecretDTO>(Encriptador.Desencriptar(jsonEncriptado, password));
            //return result;
        }
    }
}
