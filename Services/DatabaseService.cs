using Discord.WebSocket;
using Discord_Bot.Database;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.Services
{
    class DatabaseService
    {
        private readonly DiscordSocketClient _client;
        private readonly DiscordBotEntities _db;

        DatabaseService(IServiceProvider services)
        {
            _client = services.GetRequiredService<DiscordSocketClient>();
            _db = services.GetRequiredService<DiscordBotEntities>();
        }
    }
}
