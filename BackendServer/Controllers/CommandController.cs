using Microsoft.AspNetCore.Mvc;
using BackendServer.Services;
using BackendServer.Models;

namespace BackendServer.Controllers;

[ApiController]
[Route("api/command")]
public class CommandController : ControllerBase
{
    private readonly CommandService _commandService;
    private readonly LoggingService _logger;

    public CommandController(CommandService commandService, LoggingService logger)
    {
        _commandService = commandService;
        _logger = logger;
    }

    // React UI -> Backend
    // POST: api/command
    [HttpPost]
    public async Task<IActionResult> SendCommand([FromBody] CommandData commandData)
    {
        if (commandData == null || string.IsNullOrWhiteSpace(commandData.Command))
        {
            return BadRequest("Komut boş olamaz.");
        }

        _logger.Info($"HTTP Command alındı: {commandData.Command}");

        await _commandService.SendCommand(commandData.Command.ToUpper());

        return Ok(new
        {
            status = "OK",
            sentCommand = commandData.Command,
            time = DateTime.Now
        });
    }
}