using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Discord_Bot.Database;
using Discord_Bot.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Discord_Bot
{
    public class Program
    {
        private readonly IConfiguration _config;
        private DiscordSocketClient _client;
        private TaaontiaCore.TaaontiaCore _taaontia;

        static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        public Program()
        {
            // create the configuration
            var _builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile(path: "config.json");

            // build the configuration and assign to _config          
            _config = _builder.Build();
        }

        public async Task MainAsync()
        {
            using (var services = ConfigureServices())
            {
                // When working with events that have Cacheable<IMessage, ulong> parameters,
                // you must enable the message cache in your config settings if you plan to
                // use the cached message entity. 
                var client = services.GetRequiredService<DiscordSocketClient>();
                _client = client;

                // setup logging and the ready event
                services.GetRequiredService<LoggingService>();

                await services.GetRequiredService<CommandHandler>().InstallCommandsAsync();

                _taaontia = services.GetRequiredService<TaaontiaCore.TaaontiaCore>();

                await client.LoginAsync(TokenType.Bot, _config["token"]);
                await _client.StartAsync();

                _client.MessageUpdated += MessageUpdated;
                _client.Ready += () =>
                {
                    Console.WriteLine("Bot is connected!");
                    return Task.CompletedTask;
                };


                await Task.Delay(-1);
            }
        }

        private ServiceProvider ConfigureServices()
        {
            // this returns a ServiceProvider that is used later to call for those services
            // we can add types we have access to here, hence adding the new using statement:
            // using csharpi.Services;
            // the config we build is also added, which comes in handy for setting the command prefix!
            var services = new ServiceCollection()
                .AddSingleton(_config)
                .AddSingleton<DiscordSocketClient>()
                .AddSingleton<CommandService>()
                .AddSingleton<CommandHandler>()
                .AddSingleton<LoggingService>()
                .AddSingleton<TaaontiaCore.TaaontiaCore>()
                .AddDbContext<DiscordBotEntities>();

            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }


        private async Task MessageUpdated(Cacheable<IMessage, ulong> before, SocketMessage after, ISocketMessageChannel channel)
        {
            // If the message was not in the cache, downloading it will result in getting a copy of `after`.
            var message = await before.GetOrDownloadAsync();
            Console.WriteLine($"{message} -> {after}");
        }
    }
}
