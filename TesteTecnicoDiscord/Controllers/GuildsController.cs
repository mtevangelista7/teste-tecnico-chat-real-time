using Microsoft.AspNetCore.Mvc;

namespace TesteTecnicoDiscord.Controllers;

[ApiController]
[Route("[controller]")]
public class GuildsController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetGuilds()
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public async Task<IActionResult> CreateGuild(GuildRequestDto guildRequestDto)
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
    public async Task<IActionResult> CreateChannel(Guid guildId, ChannelRequestDto channelRequestDto)
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