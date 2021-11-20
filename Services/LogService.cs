using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

public class LoggingService
{
	private readonly DiscordSocketClient _client;
	private readonly CommandService _commands;


	public LoggingService(IServiceProvider services)
    {
		_client = services.GetRequiredService<DiscordSocketClient>();
		_commands = services.GetRequiredService<CommandService>();

		// hook into these events with the methods provided below
		_client.Ready += OnReadyAsync;
		_client.Log += LogAsync;
		_commands.Log += LogAsync;
	}

	public Task OnReadyAsync()
	{
		Console.WriteLine($"Connected as -> [{_client.CurrentUser}] :)");
		Console.WriteLine($"We are on [{_client.Guilds.Count}] servers");
		return Task.CompletedTask;
	}

	private Task LogAsync(LogMessage message)
	{
		if (message.Exception is CommandException cmdException)
		{
			Console.WriteLine($"[Command/{message.Severity}] {cmdException.Command.Aliases.First()}"
				+ $" failed to execute in {cmdException.Context.Channel}.");
			Console.WriteLine(cmdException);
		}
		else
			Console.WriteLine($"[General/{message.Severity}] {message}");

		return Task.CompletedTask;
	}
}
