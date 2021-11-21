using Discord.Commands;
using Discord.WebSocket;
using Discord_Bot.Database;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Text;
using Discord;

namespace Discord_Bot.Modules
{
    [RequireOwner]
    [Group("fiendType")]
    public class FiendType : ModuleBase<SocketCommandContext>
    {
        private readonly DiscordBotEntities _db;
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commandService;

        public FiendType(IServiceProvider services)
        {
            _db = services.GetRequiredService<DiscordBotEntities>();
            _client = services.GetRequiredService<DiscordSocketClient>();
            _commandService = services.GetRequiredService<CommandService>();

        }

        [Command("add")]
        [Summary("Add a fiend type to the fiend list")]
        public async Task AddFiendAsync(string name, string description = "", int baseHealth = 0, int baseEnergy = 0)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                await ReplyAsync("You have to specify at least a name for you foe.\n`!addFiend name \"description\" baseHealth baseEnergy`");
            }
            else
            {
                string trimedName = name.Trim();
                int existingFiendCount = _db.FiendType.Count(fiend => fiend.Name == trimedName);

                if (existingFiendCount > 0)
                {
                    await ReplyAsync("A for with this name already exists.");
                }
                else
                {
                    var sb = new StringBuilder();
                    var embed = new EmbedBuilder();

                    await _db.AddAsync(new Database.FiendType
                    {
                        Name = name,
                        Description = description,
                        BaseHealth = baseHealth,
                        BaseEnergy = baseEnergy,
                    });

                    await _db.SaveChangesAsync();

                    sb.AppendLine(description);
                    sb.AppendLine($"HP: {baseHealth} - SP: {baseEnergy}");

                    embed.Title = name;
                    embed.Description = sb.ToString();

                    await ReplyAsync(null, false, embed.Build());
                }
            }
        }

        [Command("edit")]
        [Summary("edit an existing fiend type")]
        public async Task EditFiendAsync(long id, Database.FiendType fiendType)
        {

            fiendType.Name = String.IsNullOrWhiteSpace(fiendType.Name) ? "" : fiendType.Name.Trim();
            int existingFiendCount = _db.FiendType.Count(fiend => fiend.Name == fiendType.Name && fiend.Id != id);

            if (existingFiendCount > 0)
            {
                await ReplyAsync("A for with this name already exists.");
            }
            else
            {
                var sb = new StringBuilder();
                var embed = new EmbedBuilder();

                var result = _db.FiendType.SingleOrDefault(type => type.Id == id);
                if (result != null)
                {
                    result.Name = String.IsNullOrWhiteSpace(fiendType.Name) ? result.Name : fiendType.Name;
                    result.Description = String.IsNullOrWhiteSpace(fiendType.Description) ? result.Description : fiendType.Description;
                    result.BaseHealth = result.BaseHealth > 0 ? result.BaseHealth : fiendType.BaseHealth;
                    result.BaseEnergy = result.BaseEnergy > 0 ? result.BaseEnergy : fiendType.BaseEnergy;
                    await _db.SaveChangesAsync();

                    sb.AppendLine($"{result.Id}: {result.Name}");
                    sb.AppendLine($"Description: {result.Description}");
                    sb.AppendLine($"Base Health: {result.BaseHealth}");
                    sb.AppendLine($"Base Energy: {result.BaseEnergy}");

                    embed.Title = "Fiend successfully updated:";
                    embed.Color = new Color(0, 0, 255);
                    embed.Description = sb.ToString();

                    await ReplyAsync(null, false, embed.Build());
                }
                else
                {
                    await ReplyAsync($"Could not find the fiend with Id {id}.");
                }

            }
        }

        [Command("list")]
        [Summary("List all registered fiend types")]
        public async Task ListFiendsAsync()
        {
            var sb = new StringBuilder();
            var embed = new EmbedBuilder();
            var fiendTypes = await _db.FiendType.ToListAsync();

            foreach (var type in fiendTypes)
            {
                sb.AppendLine($"{type.Id}: {type.Name}");
                sb.AppendLine($"Description: {type.Description}");
                sb.AppendLine($"Base Health: {type.BaseHealth}");
                sb.AppendLine($"Base Energy: {type.BaseEnergy}");
                sb.AppendLine("--");
            }

            embed.Title = "List of fiend types:";
            embed.Color = new Color(0, 255, 0);
            embed.Description = sb.ToString();

            await ReplyAsync(null, false, embed.Build());
        }
    }
}
