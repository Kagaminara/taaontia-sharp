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

        [Command]
        [Summary("Describe the current fight")]
        public async Task FightAsync()
        {
            var currentFight = await _db.GetCurrentFight(Context.User);

            if (currentFight == null)
            {
                await ReplyAsync("You are not currently in a fight !");
                return;
            }

            var character = currentFight.Allies.First();
            var ennemy = currentFight.Fiends.First();

            var eb = new EmbedBuilder();
            var sb = new StringBuilder();

            sb.AppendLine($"{character.Name}\n{character.Health} / {character.MaxHealth} HP\n{character.Health} / {character.MaxHealth} EP");
            sb.AppendLine($"{ennemy.Name}\n{ennemy.Health} / {ennemy.MaxHealth} HP\n{ennemy.Health} / {ennemy.MaxHealth} EP");

            eb.Title = "Current Fight";
            eb.Description = sb.ToString();
            await ReplyAsync(null, false, eb.Build());
        }


        [Command("engage")]
        [Summary("Engage a new foe !")]
        public async Task EngageTask()
        {
            Character connectedCharacter = await _db.FindOrCreateConnectedCharacter(Context.User);
            var currentFight = await _db.GetCurrentFight(Context.User);

            if (currentFight != null)
            {
                await ReplyAsync("You already are in a fight !");
                return;
            }

            var fiendTypes = await _db.FiendType.ToListAsync();
            if (fiendTypes.Count == 0)
            {
                await ReplyAsync("There is no fiend to fight !\nUse the !fiendType add command to add some.");
                return;
            }
            var fiendType = fiendTypes[new Random().Next(fiendTypes.Count)];

            var newFiend = new Character
            {
                Health = fiendType.BaseHealth,
                Energy = fiendType.BaseEnergy,
                MaxHealth = fiendType.BaseHealth,
                MaxEnergy = fiendType.BaseEnergy,
                Level = 1,
                Name = fiendType.Name,
                Fiend = new Fiend
                {
                    FiendType = fiendType,
                }
            };

            var fight = new Fight
            {
                Allies = new List<Character>() { connectedCharacter },
                Fiends = new List<Character>() { newFiend },
                IsActive = true,
                IsGlobal = false,
            };

            await _db.Fight.AddAsync(fight);
            await _db.Event.AddAsync(new Event
            {
                Author = connectedCharacter,
                Fight = fight,
                Type = Event.EEventType.Engage,
            });
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
            Character connectedCharacter = await _db.FindOrCreateConnectedCharacter(Context.User);
            var currentFight = await _db.GetCurrentFight(Context.User);

            if (currentFight == null)
            {
                await ReplyAsync("You are not currently in a fight !");
                return;
            }

            currentFight.IsActive = false;
            await _db.Event.AddAsync(new Event
            {
                Author = connectedCharacter,
                Fight = currentFight,
                Type = Event.EEventType.Flee,
            });
            await _db.SaveChangesAsync();

            await ReplyAsync("You successfully proved you weren't up for this.");
        }

        [Command("attack")]
        [Summary("Attack fiends")]
        public async Task AttackAsync()
        {
            var eb = new EmbedBuilder();
            var sb = new StringBuilder();
            var currentFight = await _db.GetCurrentFight(Context.User);

            if (currentFight == null)
            {
                await ReplyAsync("You are not currently in a fight.");
                return;
            }

            var character = currentFight.Allies.First();
            var ennemy = currentFight.Fiends.First();

            int characterDamage = new Random().Next(9) + 1;

            currentFight.Fiends.First().Health -= characterDamage;
            await _db.Event.AddAsync(new Event
            {
                Author = character,
                Target = ennemy,
                Value = characterDamage,
                Fight = currentFight,
                Type = Event.EEventType.Attack,
            }); ;
            await _db.Event.AddAsync(new Event
            {
                Author = character,
                Target = ennemy,
                Value = -characterDamage,
                Fight = currentFight,
                Type = Event.EEventType.HealthChange,
            });

            if (currentFight.Fiends.First().Health <= 0)
            {
                currentFight.IsActive = false;
                await _db.SaveChangesAsync();
                sb.AppendLine($"You hit the {ennemy.Name} for {characterDamage} damage !");
                sb.AppendLine($"The {ennemy.Name} is dead !");
                eb.Title = "Fight !";
                eb.Description = sb.ToString();

                await ReplyAsync(null, false, eb.Build());
                return;
            }

            int fiendDamage = new Random().Next(9) + 1;
            currentFight.Allies.First().Health -= fiendDamage;

            await _db.Event.AddAsync(new Event
            {
                Author = ennemy,
                Target = character,
                Value = fiendDamage,
                Fight = currentFight,
                Type = Event.EEventType.Attack,
            }); ;
            await _db.Event.AddAsync(new Event
            {
                Author = ennemy,
                Target = character,
                Value = -fiendDamage,
                Fight = currentFight,
                Type = Event.EEventType.HealthChange,
            });

            if (currentFight.Allies.First().Health <= 0)
            {
                currentFight.IsActive = false;
                await _db.SaveChangesAsync();
                sb.AppendLine($"The {ennemy.Name} hit you for {fiendDamage} damage !");
                sb.AppendLine($"You are dead :( !");
                eb.Title = "Fight !";
                eb.Description = sb.ToString();

                await ReplyAsync(null, false, eb.Build());
                return;
            }

            await _db.SaveChangesAsync();
            sb.AppendLine($"You hit the  you {ennemy.Name} for {characterDamage} damage !");
            sb.AppendLine($"The {ennemy.Name} hit you for {fiendDamage} damage !");
            sb.AppendLine($"{character.Name}: {character.Health} / {character.MaxHealth} HP");
            sb.AppendLine($"{ennemy.Name}: {ennemy.Health} / {ennemy.MaxHealth} HP");
            eb.Title = "Fight !";
            eb.Description = sb.ToString();

            await ReplyAsync(null, false, eb.Build());
        }

        [Command("defend")]
        [Summary("Defend from fiends")]
        public async Task DefendAsync()
        {
            var eb = new EmbedBuilder();
            var sb = new StringBuilder();
            var currentFight = await _db.GetCurrentFight(Context.User);

            if (currentFight == null)
            {
                await ReplyAsync("You are not currently in a fight.");
                return;
            }

            var character = currentFight.Allies.First();
            var ennemy = currentFight.Fiends.First();

            await _db.Event.AddAsync(new Event
            {
                Author = character,
                Fight = currentFight,
                Type = Event.EEventType.Defend,
            });

            int fiendDamage = Math.Abs(new Random().Next(9) - 5);
            currentFight.Allies.First().Health -= fiendDamage;

            await _db.Event.AddAsync(new Event
            {
                Author = ennemy,
                Target = character,
                Value = fiendDamage,
                Fight = currentFight,
                Type = Event.EEventType.Attack,
            }); ;
            await _db.Event.AddAsync(new Event
            {
                Author = ennemy,
                Target = character,
                Value = -fiendDamage,
                Fight = currentFight,
                Type = Event.EEventType.HealthChange,
            }); ;

            if (currentFight.Allies.First().Health <= 0)
            {
                currentFight.IsActive = false;
                await _db.SaveChangesAsync();
                sb.AppendLine($"The {ennemy.Name} hit you for {fiendDamage} damage !");
                sb.AppendLine($"You are dead :( !");
                eb.Title = "Fight !";
                eb.Description = sb.ToString();

                await ReplyAsync(null, false, eb.Build());
                return;
            }

            sb.AppendLine($"You enter a defensive stance !");
            sb.AppendLine($"The {ennemy.Name} hit you for {fiendDamage} damage !");
            sb.AppendLine($"{character.Name}: {character.Health} / {character.MaxHealth} HP");
            sb.AppendLine($"{ennemy.Name}: {ennemy.Health} / {ennemy.MaxHealth} HP");
            eb.Title = "Fight !";
            eb.Description = sb.ToString();

            await ReplyAsync(null, false, eb.Build());
        }
    }
}
