using login.DTOs;
using login.Models;
using login.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace login.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUsaurioServices UsaurioServices;
        private readonly IConfiguration _config;

        public LoginController(IUsaurioServices usaurioServices, IConfiguration config)
        {
            UsaurioServices = usaurioServices;
            _config = config;
        }
        [HttpPost("RegisterUser")]
        public async Task<IActionResult> PostReg([FromBody] Usuario value)
        {
            var respuesta = UsaurioServices.Save(value);
            return Ok(respuesta.Result);
        }
        // POST api/<LoginController>
        [HttpPost("loginSesion")]
        public async Task<IActionResult> PostLog([FromBody] LoginDTO value)
        {
            var respuesta = UsaurioServices.LoginSession(value, _config);
            return Ok(respuesta.Result);
        }
        // PUT api/<LoginController>/5
        [HttpPut("UpdateUser")]
        public async Task<IActionResult> Put([FromBody] Usuario value)
        {
            var respuesta = UsaurioServices.Update(value);
            return Ok(respuesta.Result);
        }

        // DELETE api/<LoginController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var respuesta = UsaurioServices.Delete(id);
            return Ok(respuesta.Result);
        }

    }
}
