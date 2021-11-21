using Discord;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.Modules
{
    public class Helper : ModuleBase<SocketCommandContext>
    {
        private readonly CommandService _commandService;

        public Helper(IServiceProvider services)
        {
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

            foreach (var module in _commandService.Modules)
            {
                string description = null;
                foreach (var cmd in module.Commands)
                {
                    var result = await cmd.CheckPreconditionsAsync(Context);
                    if (result.IsSuccess)
                    {
                        description += $"!{cmd.Aliases.First()}";
                        if (!String.IsNullOrWhiteSpace(cmd.Summary))
                        {
                            description += $" - {cmd.Summary}";
                        }
                        description += "\n";
                    }
                }
                if (!string.IsNullOrWhiteSpace(description))
                {
                    var title = $"{module.Name}";
                    if (!String.IsNullOrWhiteSpace(module.Summary))
                    {
                        title += $" - {module.Summary}";
                    }
                    builder.AddField(x =>
                    {
                        x.Name = title;
                        x.Value = description;
                        x.IsInline = false;
                    });
                }
            }

            await ReplyAsync(null, false, builder.Build());
        }

    }
}
