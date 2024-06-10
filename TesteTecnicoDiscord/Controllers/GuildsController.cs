using Mapster;
using Microsoft.AspNetCore.Mvc;
using TesteTecnicoDiscord.Application.Dtos;
using TesteTecnicoDiscord.Domain.Entities;
using TesteTecnicoDiscord.Infra.Interfaces;

namespace TesteTecnicoDiscord.Controllers;

[ApiController]
[Route("[controller]")]
public class GuildsController(IGuildsRepository guildsRepository) : ControllerBase
{
    [HttpGet("getGuilds")]
    public async Task<IActionResult> GetGuilds()
    {
        try
        {
            List<GetGuildsDto> listResponse = [];
            var listGuilds = await guildsRepository.GetAll();

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

    [HttpPost]
    public async Task<IActionResult> CreateGuild(CreateGuildRequestDto guildRequestDto)
    {
        throw new NotImplementedException();
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