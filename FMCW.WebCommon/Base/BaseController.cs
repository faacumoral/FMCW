using Microsoft.AspNetCore.Mvc;

namespace FMCW.WebCommon.Base
{
    [ApiController]
    [Route("[controller]")]
    public class BaseController : Controller
    {
        public long IdUsuario { get; set; }
        public string Jwt { get; set; }
    }
}
