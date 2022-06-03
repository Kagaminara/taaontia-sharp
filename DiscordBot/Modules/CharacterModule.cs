using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using TaaontiaCore.Events;

namespace Discord_Bot.Modules
{
    [Name("Character")]
    [Summary("Character related commands")]
    public class CharacterModule : ModuleBase<SocketCommandContext>
    {
        private readonly TaaontiaCore.TaaontiaCore _taaontia;

        public CharacterModule(IServiceProvider services)
        {
            _taaontia = services.GetRequiredService<TaaontiaCore.TaaontiaCore>();
        }

        [Command("heal")]
        [Summary("Heal yourself")]
        public async Task HealAsync()
        {
            var charResult = await _taaontia.Game.HealCharacter(new CharacterEvent()
            {
                RemoteId = Context.User.Id,
                EventType = TaaontiaCore.Enums.ECharacterEventType.REST
            });

            if (charResult.Result != TaaontiaCore.Enums.EResult.SUCCESS)
            {
                switch (charResult.Error)
                {
                    case TaaontiaCore.Enums.ECharacterError.CHARACTER_NOT_FOUND:
                        await ReplyAsync("You don't have a character yet !");
                        return;
                    case TaaontiaCore.Enums.ECharacterError.CURRENTLY_IN_FIGHT:
                        await ReplyAsync("You can't rest during a fight, durrr !");
                        return;
                    case TaaontiaCore.Enums.ECharacterError.UNKNOWN_ERROR:
                        await ReplyAsync("An unknown error occured");
                        return;
                }
            }

            await ReplyAsync($"You rested a bit, you are now at full HP !");
        }

        [Command("register")]
        [Summary("Create your character")]
        public async Task RegisterAsync()
        {
            var result = await _taaontia.Player.CreatePlayer(new NewCharacter
            {
                Name = Context.User.Username,
                RemoteId = Context.User.Id,
            });

            if (result.Result == TaaontiaCore.Enums.EResult.FAILURE)
            {
                switch (result.Error)
                {
                    case TaaontiaCore.Enums.ECharacterError.REMOTE_ID_ALREADY_EXISTS:
                        await ReplyAsync("Your character already exist !");
                        return;
                    case TaaontiaCore.Enums.ECharacterError.UNKNOWN_ERROR:
                    default:
                        await ReplyAsync("There was an error, but I don't know what exactly :/");
                        return;
                }
            }
            await ReplyAsync($"Your character named {Context.User.Username} has been created !");
        }
    }
}
