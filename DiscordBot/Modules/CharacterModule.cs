using Discord.Commands;
using Discord_Bot.Database;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.Modules
{
    [Name("Character")]
    [Summary("Character related commands")]
    public class CharacterModule: ModuleBase<SocketCommandContext>
    {
        private readonly DiscordBotEntities _db;

        public CharacterModule(IServiceProvider services)
        {
            _db = services.GetRequiredService<DiscordBotEntities>();
        }

        [Command("heal")]
        [Summary("Heal yourself")]
        public async Task HealAsync()
        {
            Character character = await _db.FindOrCreateConnectedCharacter(Context.User);

            int hpHealed = character.MaxHealth - character.Health;
            character.Health = character.MaxHealth;
            await _db.SaveChangesAsync();
            await ReplyAsync($"You healed yourself for {hpHealed}, you are now at full HP !");
        }
    }
}
