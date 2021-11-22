using Discord.Commands;
using Discord_Bot.Database;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Discord_Bot.Modules
{
    [Group("event")]
    [Name("Events")]
    [Summary("Events related commands")]
    public class EventModule: ModuleBase<SocketCommandContext>
    {
        private readonly DiscordBotEntities _db;

        public EventModule(IServiceProvider services)
        {
            _db = services.GetRequiredService<DiscordBotEntities>();
        }

        public async Task CreateEvent(Event e)
        {
            await _db.Event.AddAsync(e);
            await _db.SaveChangesAsync();
        }
    }
}
