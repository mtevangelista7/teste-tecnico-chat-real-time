using Mapster;
using Microsoft.AspNetCore.Mvc;
using TesteTecnicoDiscord.Application.Dtos;
using TesteTecnicoDiscord.Application.Interfaces.Services;
using TesteTecnicoDiscord.Client.Helper;
using TesteTecnicoDiscord.Domain.Entities;
using TesteTecnicoDiscord.Infra.Interfaces;

namespace TesteTecnicoDiscord.Controllers;

[ApiController]
[Route("[controller]")]
public class GuildsController(IGuildsService guildsService) : ControllerBase
{
    [HttpGet("getGuilds")]
    public async Task<IActionResult> GetGuilds()
    {
        try
        {
            List<GetGuildsDto> listResponse = [];
            var listGuilds = await guildsService.GetAll();

            if (listGuilds != null && listGuilds.Count > 0)
            {
                listResponse = listGuilds.Adapt<List<GetGuildsDto>>();
            }

            return Ok(listResponse);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateGuild(CreateGuildDto guildDto)
    {
        try
        {
            var guild = await guildsService.CreateNewGuild(guildDto);

            if (guild is null)
                NotFound("usuário não localizado kkkkkkkkkkk");

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{guildId:guid}")]
    public async Task<IActionResult> DeleteGuild(Guid guildId)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{guildId:guid}")]
    public async Task<IActionResult> GetAllGuildsRoms(Guid guildId)
    {
        throw new NotImplementedException();
    }

    [HttpPost("{guildId:guid}/channels")]
    public async Task<IActionResult> CreateChannel(Guid guildId, CreateChannelRequestDto channelRequestDto)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{guildId:guid}/channels/{channelId:guid}/messages/{messageId:guid}")]
    public async Task<IActionResult> DeleteMessage(Guid guildId, Guid channelId, Guid messageId)
    {
        throw new NotImplementedException();
    }

    // Não vou incluir por enquanto, pois o signalr já faz isso no hub porém vou reavaliar a lógica
    // POST	/guilds/{guildId}/channels/{channelId}/messages
    // GET	/guilds/{guildId}/channels/{channelId}
}