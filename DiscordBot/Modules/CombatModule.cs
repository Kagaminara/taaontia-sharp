using Microsoft.Extensions.DependencyInjection;
using Discord.Commands;
using Discord;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using TaaontiaCore.Events;

namespace Discord_Bot.Modules
{
    [Group("fight")]
    [Name("Fight")]
    [Summary("Combat related commands")]
    public class CombatModule : ModuleBase<SocketCommandContext>
    {
        private readonly CommandService _commandService;
        private TaaontiaCore.TaaontiaCore _taaontia;

        public CombatModule(IServiceProvider services)
        {
            _commandService = services.GetRequiredService<CommandService>();
            _taaontia = services.GetRequiredService<TaaontiaCore.TaaontiaCore>();
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
            var result = await _taaontia.Fight.GetFight(new FightEvent()
            {
                RemoteId = Context.User.Id
            });

            if (result.Result != TaaontiaCore.Enums.EResult.SUCCESS)
            {
                switch (result.Error)
                {
                    case TaaontiaCore.Enums.EFightError.NO_CURRENT_FIGHT:
                        await ReplyAsync("You aren't currently in a fight.");
                        return;
                    default:
                        await ReplyAsync("An error occured :(");
                        return;
                }
            }

            var currentFight = result.Fight;

            var eb = new EmbedBuilder();
            var sb = new StringBuilder();

            sb.AppendLine($"{currentFight.Player.Name}\n{currentFight.Player.Health} / {currentFight.Player.MaxHealth} HP\n{currentFight.Player.Energy} / {currentFight.Player.MaxEnergy} EP");
            sb.AppendLine($"{currentFight.Fiend.Name}\n{currentFight.Fiend.Health} / {currentFight.Fiend.MaxHealth} HP\n{currentFight.Fiend.Energy} / {currentFight.Fiend.MaxEnergy} EP");

            eb.Title = "Current Fight";
            eb.Description = sb.ToString();
            await ReplyAsync(null, false, eb.Build());
        }


        [Command("engage")]
        [Summary("Engage a new foe !")]
        public async Task EngageTask()
        {
            var result = await _taaontia.Fight.Engage(new NewFightEvent
            {
                RemoteId = Context.User.Id,
            }
                );

            if (result.Error == TaaontiaCore.Enums.EFightError.ALREADY_IN_FIGHT)
            {
                await ReplyAsync("You are already in a fight !");
                return;
            }

            var eb = new EmbedBuilder();
            var sb = new StringBuilder();

            sb.AppendLine($"Health: {result.Fight.Fiend.Health}");
            sb.AppendLine($"Energy: {result.Fight.Fiend.Energy}");

            eb.Title = $"A wild {result.Fight.Fiend.Name} appears !";
            eb.Description = sb.ToString();

            await ReplyAsync(null, false, eb.Build());
        }

        [Command("flee")]
        [Summary("Escape from your current fight like the coward you are")]
        public async Task FleeAsync()
        {
            var playerFleeResult = await _taaontia.Fight.Flee(new FightEvent
            {
                RemoteId = Context.User.Id,
            }
);
            if (playerFleeResult.Result == TaaontiaCore.Enums.EResult.SUCCESS)
            {
                await ReplyAsync("You successfully proved you weren't up for this.");

            }
            else
            {
                if (playerFleeResult.Error == TaaontiaCore.Enums.EFightError.NO_CURRENT_FIGHT)
                {
                    await ReplyAsync("You aren't currently in a fight.");
                }
            }
        }

        [Command("attack")]
        [Summary("Attack fiends")]
        public async Task AttackAsync()
        {
            var eb = new EmbedBuilder();
            var sb = new StringBuilder();

            var playerAttackResult = await _taaontia.Fight.Action(new FightEvent
            {
                RemoteId = Context.User.Id,
                Target = TaaontiaCore.Enums.EFightEventTarget.FIEND,
                // TODO: Use skills that the player has actually access to
                SkillId = 1
            }
    );
            if (playerAttackResult.Result != TaaontiaCore.Enums.EResult.SUCCESS)
            {
                switch (playerAttackResult.Error)
                {
                    case TaaontiaCore.Enums.EFightError.NO_CURRENT_FIGHT:
                        await ReplyAsync("You aren't currently in a fight.");
                        return;
                    default:
                        await ReplyAsync("An error occured.");
                        return;
                }
            }
            FightResult fiendAttackResult = null;
            if (playerAttackResult.Fight.IsActive)
            {
                fiendAttackResult = await _taaontia.Fight.Action(new FightEvent
                {
                    RemoteId = Context.User.Id,
                    Target = TaaontiaCore.Enums.EFightEventTarget.PLAYER,
                    // TODO: Use skills that the fiend has actually access to
                    SkillId = 1
                }
                    );
            }
            var ennemy = playerAttackResult.Fight.Fiend;
            var character = playerAttackResult.Fight.Player;

            eb.Title = "Fight !";
            sb.AppendLine($"You hit the {ennemy.Name} for {playerAttackResult.TargetDamage} damage !");
            if (playerAttackResult.Fight.IsActive)
            {
                sb.AppendLine($"The {ennemy.Name} hit you for {fiendAttackResult.TargetDamage} damage !");
            }
            sb.AppendLine($"{character.Name}: {character.Health} / {character.MaxHealth} HP");
            sb.AppendLine($"{ennemy.Name}: {ennemy.Health} / {ennemy.MaxHealth} HP");

            if (!playerAttackResult.Fight.IsActive)
            {
                var rewards = playerAttackResult.Rewards;
                sb.AppendLine($"\nYou won !");
                sb.AppendLine($"You gained {rewards.Experience} exp !");
                sb.AppendLine($"You found {rewards.Currency} gold !");
            }
            
            eb.Description = sb.ToString();
            await ReplyAsync(null, false, eb.Build());
        }

        //[Command("defend")]
        //[Summary("Defend from fiends")]
        //public async Task DefendAsync()
        //{
        //    var eb = new EmbedBuilder();
        //    var sb = new StringBuilder();
        //    var currentFight = await _db.GetCurrentFight(Context.User);

        //    if (currentFight == null)
        //    {
        //        await ReplyAsync("You are not currently in a fight.");
        //        return;
        //    }

        //    var character = currentFight.Allies.First();
        //    var ennemy = currentFight.Fiends.First();

        //    await _db.Event.AddAsync(new Event
        //    {
        //        Author = character,
        //        Fight = currentFight,
        //        Type = Event.EEventType.Defend,
        //    });

        //    int fiendDamage = Math.Abs(new Random().Next(9) - 5);
        //    currentFight.Allies.First().Health -= fiendDamage;

        //    await _db.Event.AddAsync(new Event
        //    {
        //        Author = ennemy,
        //        Target = character,
        //        Value = fiendDamage,
        //        Fight = currentFight,
        //        Type = Event.EEventType.Attack,
        //    }); ;
        //    await _db.Event.AddAsync(new Event
        //    {
        //        Author = ennemy,
        //        Target = character,
        //        Value = -fiendDamage,
        //        Fight = currentFight,
        //        Type = Event.EEventType.HealthChange,
        //    }); ;

        //    if (currentFight.Allies.First().Health <= 0)
        //    {
        //        currentFight.IsActive = false;
        //        await _db.SaveChangesAsync();
        //        sb.AppendLine($"The {ennemy.Name} hit you for {fiendDamage} damage !");
        //        sb.AppendLine($"You are dead :( !");
        //        eb.Title = "Fight !";
        //        eb.Description = sb.ToString();

        //        await ReplyAsync(null, false, eb.Build());
        //        return;
        //    }

        //    sb.AppendLine($"You enter a defensive stance !");
        //    sb.AppendLine($"The {ennemy.Name} hit you for {fiendDamage} damage !");
        //    sb.AppendLine($"{character.Name}: {character.Health} / {character.MaxHealth} HP");
        //    sb.AppendLine($"{ennemy.Name}: {ennemy.Health} / {ennemy.MaxHealth} HP");
        //    eb.Title = "Fight !";
        //    eb.Description = sb.ToString();

        //    await ReplyAsync(null, false, eb.Build());
        //}
    }
}
