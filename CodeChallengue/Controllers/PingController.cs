using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.Controllers
{
    /// <summary>
    /// Clase controlador encargada de hacer pong cuando alguien escriba /ping
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class PingController : ControllerBase
    {
        /// <summary>
        /// Función Get() encargada de devolver pong, solo genera estados 200.
        /// </summary>
        /// <returns>Devuelve un Status200OK y string llamado "Pong"</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<string> Get()
        {
            return "Pong";
        }
    }
}
