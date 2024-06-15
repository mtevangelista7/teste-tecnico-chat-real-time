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
public class GuildsController(
    IGuildsService guildsService,
    IChannelService channelService,
    IMessageService messageService,
    IUserService userService) : ControllerBase
{
    [HttpGet("getGuilds")]
    public async Task<IActionResult> GetGuilds()
    {
        try
        {
            List<GetGuildsDto> listResponse = [];
            var listGuilds = await guildsService.GetAll();

            if (listGuilds is { Count: > 0 })
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

    [HttpGet("getGuild/{guildId:guid}")]
    public async Task<IActionResult> GetGuild(Guid guildId)
    {
        try
        {
            var guild = await guildsService.GetById(guildId);

            if (guild is null)
            {
                return BadRequest();
            }

            var guildResponse = guild.Adapt<GetGuildsDto>();
            return Ok(guildResponse);
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
        try
        {
            // TODO: Verificar se o cascade está funcionando corretamente nas tabelas de junção
            await guildsService.Delete(guildId);
            return Accepted();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("getChannels/{guildId:guid}")]
    public async Task<IActionResult> GetAllGuildsRoms(Guid guildId)
    {
        try
        {
            List<GetChannelsDto> listResponse = [];
            var listChannels = await channelService.GetAllChannelsById(guildId);

            if (listChannels is { Count: > 0 })
            {
                listResponse = listChannels.Adapt<List<GetChannelsDto>>();
            }

            return Ok(listResponse);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{guildId:guid}/channels")]
    public async Task<IActionResult> CreateChannel(Guid guildId, CreateChannelDto request)
    {
        try
        {
            var newChannel = await channelService.CreateNewChannel(request);

            if (newChannel is null)
                NotFound("servidor não localizado kkkkkkkkkkk");

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // TODO: coloca isso no controller de mensagens e muda essa rota pelo amor de DEUS
    [HttpDelete("{guildId:guid}/channels/{channelId:guid}/messages/{messageId:guid}")]
    public async Task<IActionResult> DeleteMessage(Guid guildId, Guid channelId, Guid messageId)
    {
        try
        {
            await messageService.Delete(messageId);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("channels/{channelId:guid}/getMessages")]
    public async Task<IActionResult> GetMessagesFromChannel(Guid channelId)
    {
        try
        {
            List<ReceiveMessageDto> receiveMessagesDto = [];
            var messages = await messageService.GetByChannelId(channelId);

            if (messages is not { Count: > 0 }) return Ok(receiveMessagesDto);

            receiveMessagesDto = messages.Adapt<List<ReceiveMessageDto>>();
            receiveMessagesDto.ForEach(async x => x.OwnerUsername = (await userService.GetById(x.UserId)).Username);
            return Ok(receiveMessagesDto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("/guilds/channels/{id:guid}")]
    public async Task<IActionResult> GetChannel(Guid id)
    {
        try
        {
            var channel = await channelService.GetById(id);

            if (channel is null) return NotFound();

            var channelResponse = channel.Adapt<GetChannelsDto>();
            return Ok(channelResponse);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("/{guildId:guid}/{userId:guid}/addUser")]
    public async Task<IActionResult> AddUserToGuild(Guid guildId, Guid userId)
    {
        try
        {
            await guildsService.AddUserToGuild(userId, guildId);

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("/{channelId:guid}/{userId:guid}/addUserToChannel")]
    public async Task<IActionResult> AddUserToChannel(Guid channelId, Guid userId)
    {
        try
        {
            await channelService.AddUserToChannel(userId, channelId);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}