using Discord.Commands;
using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.Modules
{
    [Group("fight")]
    public class Combat: ModuleBase<SocketCommandContext>
    {
        [Command("attack")]
        public async Task AttackAsync()
        {
            int damage = new Random().Next(10);

            await ReplyAsync($"You dealt {damage} damage to whatever you attacked !");
        }
    }
}
