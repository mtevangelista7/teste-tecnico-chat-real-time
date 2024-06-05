using Microsoft.AspNetCore.Mvc;

namespace TesteTecnicoDiscord.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAuthUser()
    {
        throw new NotImplementedException();
    }
}