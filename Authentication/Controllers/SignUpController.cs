using Authentication.Model;
using Authentication.Services.SignUpService;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignUpController(ISignUpService service) : ControllerBase
    {
        [HttpPost]
        [Route("signUp")]
        public IActionResult SignUp([FromBody]User user)
        {
            if (service.CompleteSignUp(user))
            {
                return Created();
            }
            return BadRequest("Please Check logs for details.");
        }

        [HttpPost]
        [Route("secureSignUp")]
        public IActionResult SecureSignUp([FromBody] User user)
        {
            if (service.CompleteSignUp(user))
            {
                return Created();
            }
            return BadRequest("Please Check logs for details.");
        }
    }
}
