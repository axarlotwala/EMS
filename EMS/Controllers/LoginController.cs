using EMS.DataAccess.Interface;
using EMS.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EMS.Controllers
{
    [Route("api/Login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        ILoginRepository repository;

        public LoginController(ILoginRepository _repository)
        {
            repository = _repository;
        }

        [HttpPost]
        [Route("UserLogin")]
        public async Task<IActionResult> UserLogin([FromBody] RequestLogin request)
        {
            var result = await repository.UserLogin(request);
            return Ok(result);
        }
    }
}
