using Discord.Commands;
using Discord;
using System;
using System.Linq;
using System.Threading.Tasks;
using Discord_Bot.Database;
using Microsoft.Extensions.DependencyInjection;
using Discord.WebSocket;

namespace Discord_Bot.Modules
{
    [Group("fight")]
    [Summary("Combat related commands")]
    public class Combat : ModuleBase<SocketCommandContext>
    {
        private readonly DiscordBotEntities _db;
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commandService;

        public Combat(IServiceProvider services)
        {
            _db = services.GetRequiredService<DiscordBotEntities>();
            _client = services.GetRequiredService<DiscordSocketClient>();
            _commandService = services.GetRequiredService<CommandService>();

        }

        [Command("help")]
        [Summary("Display help text")]
        public async Task HelpAsync()
        {
            var builder = new EmbedBuilder()
            {
                Color = new Color(114, 137, 218),
                Description = "These are the commands you can use"
            };

            var module = _commandService.Modules.Where(module => module.Name == "fight").First();
            string description = null;
            foreach (var cmd in module.Commands)
            {
                var result = await cmd.CheckPreconditionsAsync(Context);
                if (result.IsSuccess)
                    description += $"!{cmd.Aliases.First()} - {cmd.Summary}\n";
            }
            if (!string.IsNullOrWhiteSpace(description))
            {
                builder.AddField(x =>
                {
                    x.Name = $"{module.Name} - {module.Summary}";
                    x.Value = description;
                    x.IsInline = false;
                });
            }
            await ReplyAsync(null, false, builder.Build());
        }

        [Command("engage")]
        [Summary("Engage a new foe !")]
        public async Task EngageTask()
        {

            await ReplyAsync("pouet");
        }

        [Command("attack")]
        [Summary("Attack fiends")]
        public async Task AttackAsync()
        {
            int damage = new Random().Next(10);

            await ReplyAsync($"You dealt {damage} damage to whatever you attacked !");
        }

        [Command("defend")]
        [Summary("Defend from fiends")]
        public async Task DefendAsync()
        {
            int damage = new Random().Next(10);

            await ReplyAsync($"You parried {damage} damage fromwhatever attacked you !");
        }
    }
}
