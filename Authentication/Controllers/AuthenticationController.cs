using Authentication.Model;
using Authentication.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(IAuthService service) : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        public IActionResult GetCredentialsAndValidate([FromBody]User user)
        {
            return Ok(service.Validate(user) ? "Login Successful": "Incorrect credentials");
        }
    }
}
