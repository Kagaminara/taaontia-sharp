using Microsoft.Extensions.DependencyInjection;
using Discord.Commands;
using Discord;
using System;
using System.Threading.Tasks;
using Discord_Bot.Database;
using Discord.WebSocket;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Discord_Bot.Modules
{
    [Group("fight")]
    [Name("Fight")]
    [Summary("Combat related commands")]
    public class CombatModule : ModuleBase<SocketCommandContext>
    {
        private readonly DiscordBotEntities _db;
        private readonly CommandService _commandService;

        public CombatModule(IServiceProvider services)
        {
            _db = services.GetRequiredService<DiscordBotEntities>();
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
            Character connectedCharacter = await _db.FindOrCreateConnectedCharacter(Context.User);
            var currentFight = await _db.GetCurrentCombat(Context.User);

            if (currentFight != null)
            {
                await ReplyAsync("You already are in a fight !");
                return;
            }

            var fiendTypes = await _db.FiendType.ToListAsync();
            var fiendType = fiendTypes[new Random().Next(fiendTypes.Count)];

            var newFiend = new Fiend
            {
                FiendType = fiendType,
                Health = fiendType.BaseHealth,
                Energy = fiendType.BaseEnergy,
                Level = 1,
                Name = fiendType.Name,
            };

            var fight = new Fight
            {
                Allies = new List<Character>() { connectedCharacter },
                Fiends = new List<Fiend>() { newFiend },
                IsActive = true,
                IsGlobal = false,
            };

            await _db.Fight.AddAsync(fight);
            await _db.SaveChangesAsync();

            var eb = new EmbedBuilder();
            var sb = new StringBuilder();

            sb.AppendLine($"Health: {newFiend.Health}");
            sb.AppendLine($"Energy: {newFiend.Energy}");

            eb.Title = $"A wild {fiendType.Name} appears !";
            eb.Description = sb.ToString();

            await ReplyAsync(null, false, eb.Build());
        }

        [Command("flee")]
        [Summary("Escape from your current fight like the coward you are")]
        public async Task FleeAsync()
        {
            var currentFight = await _db.GetCurrentCombat(Context.User);

            if (currentFight == null)
            {
                await ReplyAsync("You are not currently in a fight !");
                return;
            }

            currentFight.IsActive = false;
            await _db.SaveChangesAsync();

            await ReplyAsync("You successfully proved you weren't up for this.");
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
