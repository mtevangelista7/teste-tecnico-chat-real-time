using System.Security.Claims;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TesteTecnicoDiscord.Application.Dtos;
using TesteTecnicoDiscord.Application.Interfaces.Services;

namespace TesteTecnicoDiscord.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UserController(IUserService userService, IMessageService messageService, IGuildsService guildsService)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAuthUser()
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(userId))
                return BadRequest();

            var user = await userService.GetById(Guid.Parse(userId));

            if (user is null)
                return BadRequest();

            var userDto = user.Adapt<GetUserDto>();

            return Ok(userDto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("getMessagesCount/{userId:guid}")]
    public async Task<IActionResult> GetMessagesCountFromUser(Guid userId)
    {
        try
        {
            return Ok(await messageService.GetMessageCountFromUser(userId));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("getGuildsCount/{userId:guid}")]
    public async Task<IActionResult> GetGuildCountFromUser(Guid userId)
    {
        try
        {
            return Ok(await guildsService.GetGuildCountFromUser(userId));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}